package main

import (
	_ "embed"
	"os"

	"github.com/getlantern/systray"
)

//go:embed assets\icon.ico
var iconData []byte

func setupTray() {
	systray.Run(onReady, onExit)
}

var consoleVisible = false

func onReady() {
	systray.SetIcon(iconData)
	systray.SetTitle("weathery-backend")
	systray.SetTooltip("weathery backend is running")

	// add menu items
	mQuit := systray.AddMenuItem("Quit", "Stop the backend and exit")
	mToggleConsole := systray.AddMenuItem("Show Console", "Show or hide the console")

	go func() {
		for {
			select {
			case <-mToggleConsole.ClickedCh:
				consoleVisible = !consoleVisible
				showConsole(consoleVisible)

				if consoleVisible {
					mToggleConsole.SetTitle("Hide Console")
				} else {
					mToggleConsole.SetTitle("Show Console")
				}
			case <-mQuit.ClickedCh:
				systray.Quit()
				os.Exit(0)
				return
			}
		}
	}()
}

func onExit() {
	freeConsole.Call()
}
