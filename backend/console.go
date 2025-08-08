package main

import (
	"syscall"
)

var (
	kernel32         = syscall.NewLazyDLL("kernel32.dll")
	user32           = syscall.NewLazyDLL("user32.dll")
	getConsoleWindow = kernel32.NewProc("GetConsoleWindow")
	showWindow       = user32.NewProc("ShowWindow")
	allocConsole     = kernel32.NewProc("AllocConsole")
	freeConsole      = kernel32.NewProc("FreeConsole")
)

const (
	SW_HIDE = 0
	SW_SHOW = 5
)

func getConsoleWindowHandle() uintptr {
	ret, _, _ := getConsoleWindow.Call()
	return ret
}

func showConsole(show bool) {
	hwnd := getConsoleWindowHandle()

	if show {
		if hwnd == 0 {
			// console does not exist. allocate, and create!
			allocConsole.Call()
			attachConsoleStdio()
			// reassign hwnd and get the new console window handle
			hwnd = getConsoleWindowHandle()
			// show the console window immediately
			showWindow.Call(hwnd, SW_SHOW)
		} else {
			// console is in existence so just show!
			showWindow.Call(hwnd, SW_SHOW)
		}
	} else {
		if hwnd != 0 {
			// console exists so hide it
			showWindow.Call(hwnd, SW_HIDE)
		}
	}
}
