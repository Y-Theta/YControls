#pragma once
//#define _SPDLOG_
#define WIN32_LEAN_AND_MEAN     
#include <iostream>
#include <stdio.h>
#include <string>
#include <process.h>

#include <windows.h>
#include <commctrl.h>
#pragma comment(lib, "Comctl32.lib")

#ifdef _SPDLOG_
#include <spdlog/spdlog.h>
#include <spdlog/async.h>
#include <spdlog/sinks/rotating_file_sink.h>
#endif