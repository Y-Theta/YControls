// dllmain.cpp : 定义 DLL 应用程序的入口点。
#include "pch.h"
#include "dllmain.h"

using namespace std;

BOOL APIENTRY DllMain (HMODULE hModule,
	DWORD  ul_reason_for_call,
	LPVOID lpReserved
) {
	_mid = hModule;
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		Initlog ();
		LOG ("Process ATTACH  mid : {}", (void*)_mid);
		break;
	case DLL_THREAD_ATTACH:break;
	case DLL_THREAD_DETACH:break;
	case DLL_PROCESS_DETACH:
		LOG ("Process DETACH begin", (void*)_mid);
		if (g_exit_lock) {
			ReleaseSemaphore (g_exit_lock, 1, NULL);
			CloseHandle (g_exit_lock);
		}
		LOG ("Process DETACH  mid : {}", (void*)_mid);
#ifdef  _SPDLOG_
		spdlog::drop_all ();
#endif //  DLOG
		break;
	}
	return TRUE;
}

#ifdef _SPDLOG_
shared_ptr<spdlog::logger> _logger;
#endif // 

#pragma region SPDLOG
template<typename FormatString, typename... Args>
void LOG (const FormatString& fmt, const Args &...args) {
#ifdef  _SPDLOG_
	_logger->info (fmt, args...);
	_logger->flush ();
#endif //  DLOG
}
void Initlog () {
	//FLAGS_log_dir = strPath.substr (0, pos);
	//spdlog: ("log", "E:\\Log\\log");
#ifdef  _SPDLOG_
	_logger = spdlog::rotating_logger_mt ("ndhlog", "E:\\Log\\ndh.log", 1024 * 100, 2);
#endif //  DLOG
}
#pragma endregion

void Hook (HWND pin) {
	_Phinstance = pin;
	LOG ("Hook {}", (void*)_Phinstance);
	if (_hid)
		UnHook ();
	if (!_hid) {
		char classname[80];
		_shelltraywnd = FindWindowExA (NULL, NULL, "shell_traywnd", NULL);
		if (_shelltraywnd) {
			_traynotifywnd = FindWindowExA (_shelltraywnd, NULL, "traynotifywnd", NULL);
			if (_traynotifywnd) {
				HWND child = _traynotifywnd;
				for (child = GetWindow (_traynotifywnd, GW_CHILD); child; child = GetWindow (child, GW_HWNDNEXT))
				{
					GetClassNameA (child, classname, _countof (classname));
					if (!_strcmpi (classname, "TrayInputIndicatorWClass")) {
						_tohook = child;
						break;
					}
				}
				DWORD dwThreadId = GetWindowThreadProcessId (_shelltraywnd, NULL);
				if (dwThreadId) {
					_hid = SetWindowsHookEx (WH_CALLWNDPROC, ONPROC, _mid, dwThreadId);
					if (_hid && _tohook) {
						_VirtualRect.left = _VirtualRect.top = 48;
						PostMessage (_shelltraywnd, WM_SIZE, SIZE_RESTORED, 0);
						SendMessage (_tohook, WM_NULL, 0, 0);
					}
				}
			}
		}
	}
}

void UnHook () {
	//Removeclasshook ();
	if (_hid) {
		UnhookWindowsHookEx (_hid);
		_hid = NULL;
	}
}

EXTERN_C IMAGE_DOS_HEADER __ImageBase;
static void SelfDestruct (void* user)
{ // never crashed without this SelfDestruct stub, but better safe then sorry :P Crashing the explorer isn't that nice.
	(void)user;
	// make sure we're out of our hooked message loop
	SendMessage (_gs_tray, WM_NULL, 0, 0);
	InvalidateRect (_Instance, NULL, TRUE);
	SendMessage (_Instance, WM_NULL, 0, 0);
	// refresh primary taskbar
	InvalidateRect (_gs_taskbar, NULL, TRUE);
	SendMessage (_gs_taskbar, WM_SIZE, SIZE_RESTORED, 0);
	// we could use FreeLibraryAndExitThread on XP+, but this "hack" should be ok for now.
	_gs_taskbar = _gs_tray = NULL;
	_Instance = NULL;
	CreateThread (NULL, 0, (LPTHREAD_START_ROUTINE)FreeLibrary, &__ImageBase, 0, NULL); // die painfully
	//FreeLibraryAndExitThread (_mid, 0);
	LOG ("SelfDestruct");
}

void Exit () {
	LOG ("Begin Exit");
	if (_Instance && IsWindow (_Instance)) {
		HANDLE lock;
		// Call the module proc to end
		SendMessage (_Instance, WM_COMMAND, IDM_EXIT, 0);
		//lock = OpenSemaphore (SYNCHRONIZE | SEMAPHORE_MODIFY_STATE, 0, kConfigName + 1);
		//WaitForSingleObject (lock, INFINITE);
		//LOG ("Exit Wait");
		//ReleaseSemaphore (lock, 1, NULL);
		//CloseHandle (lock);
		//Sleep (1); // hopefully useless sleep
		//LOG ("Exit");
	}
}

void End () {
	g_exit_lock = CreateSemaphore (NULL, 1, 1, kConfigName + 1);
	WaitForSingleObject (g_exit_lock, 0);
	LOG ("End Wait");
	RemoveWindowSubclass (_gs_tray, ShellTray_PROC, 0);
	RemoveWindowSubclass (_Instance, WndTray_PROC, 0);
	_beginthread (SelfDestruct, 0, NULL);
	LOG ("End");
}

/// <summary>
/// This procedure controls the inject
/// </summary>
LRESULT CALLBACK ONPROC (int nCode, WPARAM wParam, LPARAM lParam) {
	CWPSTRUCT* cwp = (CWPSTRUCT*)lParam;
	if (nCode >= 0 && cwp && cwp->hwnd) {
		char classname[80];
		if (!_Instance && GetClassNameA (cwp->hwnd, classname, 80) && !_strcmpi (classname, "TrayInputIndicatorWClass")) {
			_Instance = cwp->hwnd;

			HMODULE mod = LoadLibraryA ("hsystrayembed"); // self-reference

			_gs_tray = GetParent (cwp->hwnd);
			_gs_taskbar = GetParent (_gs_tray);
			SetWindowSubclass (_gs_tray, ShellTray_PROC, 0, 0);
			SetWindowSubclass (cwp->hwnd, WndTray_PROC, 0, 0);

			PostMessage (_gs_taskbar, WM_SIZE, SIZE_RESTORED, 0);
			InvalidateRect (_gs_taskbar, NULL, 1);
			// To main window to unhook and save its handle
			PostMessage (_Phinstance, WMC_INIT, 0, (LPARAM)cwp->hwnd);
		}
	}
	return CallNextHookEx (NULL, nCode, wParam, lParam);
}

/// <summary>
/// This procedure controls the size and arrange
/// </summary>
static LRESULT CALLBACK ShellTray_PROC (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam,
	UINT_PTR uIdSubclass, DWORD_PTR dwRefData) {
	switch (message) {
	case WM_TRAY: {
		union {
			struct {
				int16_t width;
				int16_t height;
			} part;
			LRESULT combined;
		} size;
		size.combined = DefSubclassProc (hwnd, message, wParam, lParam);
		if (_horizental)
			size.part.width += (int16_t)(_VirtualRect.left);
		else
			size.part.height += (int16_t)(_VirtualRect.top);
#ifdef _SPDLOG_SIZE_
		LOG ("WM_TRAY  size {}-{} orientation :{}", size.part.width, size.part.height, _horizental);
#endif 
		return size.combined;
		break;
	}
	case WM_NOTIFY: {
		if (!_rotating) {

			NMHDR* nmh = (NMHDR*)lParam;
			LRESULT ret;
			RECT sibling_rc, taskbar, pos1, pos2;
			HWND sibling;
			int syspager_pos, next_pos;
			if (nmh->code != PGN_CALCSIZE || !_Phinstance)
				break;
			ret = DefSubclassProc (hwnd, message, wParam, lParam);
			GetClientRect (_Instance, &sibling_rc);
			MapWindowPoints (_Instance, hwnd, (POINT*)&sibling_rc, 1);
#ifdef _SPDLOG_SIZE_
			LOG ("WM_NOTIFY  {}-{}-{}-{}", sibling_rc.left, sibling_rc.top, sibling_rc.right, sibling_rc.bottom);
#endif 
			GetClientRect (_gs_taskbar, &taskbar);
			GetWindowRect (_Instance, &pos1);
			_horizental = (taskbar.right - taskbar.left) > (taskbar.bottom - taskbar.top);
			if (_horizental) {
				syspager_pos = sibling_rc.left;
				next_pos = sibling_rc.left + sibling_rc.right + _VirtualRect.left;
				SetWindowPos (_Phinstance, (HWND)-1, pos1.left + sibling_rc.right, pos1.top, 0, 0, SWP_NOACTIVATE | SWP_NOSIZE);
			}
			else {
				syspager_pos = sibling_rc.top;
				next_pos = sibling_rc.top + sibling_rc.bottom + _VirtualRect.top;
				SetWindowPos (_Phinstance, (HWND)-1, pos1.left, pos1.top + sibling_rc.bottom, 0, 0, SWP_NOACTIVATE | SWP_NOSIZE);
			}
			for (sibling = GetWindow (_Instance, GW_HWNDNEXT); sibling; sibling = GetWindow (sibling, GW_HWNDNEXT)) {
				GetClientRect (sibling, &sibling_rc);
				MapWindowPoints (sibling, hwnd, (POINT*)&sibling_rc, 1);
				if (_horizental) {
					if (sibling_rc.left < syspager_pos) // Win10 orders the controls properly, but others don't
						continue;
					sibling_rc.left = next_pos;
					next_pos += sibling_rc.right;
				}
				else {
					if (sibling_rc.top < syspager_pos)
						continue;
					sibling_rc.top = next_pos;
					next_pos += sibling_rc.bottom;
				}
				SetWindowPos (sibling, 0, sibling_rc.left, sibling_rc.top, 0, 0, SWP_NOACTIVATE | SWP_NOZORDER | SWP_NOSIZE);
			}
			PostMessage (_Phinstance, WMC_SIZE, 0, 0);
			return ret;
		}
	}
	case WM_LBUTTONDOWN:
		_rotating = TRUE;
		break;
	case WM_LBUTTONUP:
		_rotating = FALSE;
	default:
		break;
	}
	return DefSubclassProc (hwnd, message, wParam, lParam);
}

/// <summary>
/// This procedure controls the exit and resource free 
/// </summary>
static LRESULT CALLBACK WndTray_PROC (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam,
	UINT_PTR uIdSubclass, DWORD_PTR dwRefData) {
	switch (message)
	{
	case WM_DESTROY:
		RemoveWindowSubclass (_gs_tray, ShellTray_PROC, 0);
		RemoveWindowSubclass (hwnd, WndTray_PROC, uIdSubclass);
		UnHook ();
		break;
	case WMC_SIZE:
		_VirtualRect.left = HIWORD (lParam);
		_VirtualRect.top = LOWORD (lParam);
#ifdef _SPDLOG_
		LOG ("WMC_SIZE  {}-{}", _VirtualRect.left, _VirtualRect.top);
#endif 
		PostMessage (_gs_taskbar, WM_SIZE, SIZE_RESTORED, 0);
		InvalidateRect (_gs_taskbar, NULL, 1);
		return 1;
	case WM_COMMAND:
		switch (LOWORD (wParam)) {
		case IDM_EXIT:
			End ();
			break;
		case IDM_SHUTDOWN: {
			break; }
		}
		return 0;
		break;
	default:
		break;
	}
	return DefSubclassProc (hwnd, message, wParam, lParam);
}
