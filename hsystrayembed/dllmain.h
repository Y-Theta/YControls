#pragma once
#include "pch.h"
#pragma region CONST

#define WM_TRAY                        (WM_USER + 100)
#define WMC_INIT                       (WM_USER + 1)
#define WMC_SIZE                       (WM_USER + 2)
#define WMC_NEW                        (WM_USER + 3)

#define IDM_EXIT                        289
#define IDM_SHUTDOWN                    252

const wchar_t* kConfigName = L"sign..";

#pragma endregion

#pragma region SHARED VAR
#ifdef __GNUC__
#	define SHARED __attribute__((section(".shared"),shared))
#else
#	pragma data_seg(".shared")
#   pragma comment(linker,"/SECTION:.shared,RWS")
#endif

RECT _VirtualRect = { 0 };

HINSTANCE _mid;
HHOOK _hid = NULL;
HWND _Phinstance = NULL;
HWND _Instance = NULL;

HWND _shelltraywnd = NULL;
HWND _tohook = NULL;
HWND _gs_tray = NULL;
HWND _gs_clock = NULL;
HWND _gs_taskbar = NULL;
HWND _gs_rebar = NULL;

bool _rotating = FALSE;
bool _redraw = FALSE;

#ifndef __GNUC__
#	pragma data_seg()
#endif

HANDLE g_exit_lock;
static bool _horizental = TRUE;
HWND _traynotifywnd = NULL;

typedef struct LinkedList {
	POINT p;
	HWND h;
	LinkedList* next;
}*PLIST, LIST;

#pragma endregion

typedef void (*HOOKFUN)(int code, WPARAM wParam, LPARAM lParam);

extern "C" __declspec(dllexport) void Hook (HWND pin);
extern "C" __declspec(dllexport) void UnHook ();
extern "C" __declspec(dllexport) void Exit ();

static LRESULT CALLBACK ShellClock_PROC (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam,
	UINT_PTR uIdSubclass, DWORD_PTR dwRefData);
static LRESULT CALLBACK ShellTask_PROC (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam,
	UINT_PTR uIdSubclass, DWORD_PTR dwRefData);
LRESULT CALLBACK ONPROC (int nCode, WPARAM wParam, LPARAM lParam);
static LRESULT CALLBACK ShellTray_PROC (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam,
	UINT_PTR uIdSubclass, DWORD_PTR dwRefData);
static LRESULT CALLBACK WndTray_PROC (HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam,
	UINT_PTR uIdSubclass, DWORD_PTR dwRefData);

void Initlog ();
template<typename FormatString, typename... Args>
void LOG (const FormatString& fmt, const Args &...args);
