using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WindowManager : SingletonMono<WindowManager>
{
	[SerializeField] private Window _currentWindow;
	[SerializeField] private List<WindowHolder> _windowsInitilization = new List<WindowHolder>();
	private Dictionary<Window, WindowHolder> _windows = new Dictionary<Window, WindowHolder>();

	public override void Awake()
	{
		base.Awake();
		_windowsInitilization.ForEach(window =>
		{
			SubscribeWindow(window, false);
		});
	}

	public void SubscribeWindow(WindowHolder windowHolder, bool isInit = true)
	{
		if(_windows.ContainsKey(windowHolder.Window) == false) _windows.Add(windowHolder.Window, windowHolder);
		if (isInit) windowHolder.onOpenWindow += HandleCurrentActiveWindow;
		
	}

	public void HandleCurrentActiveWindow(Window window)
	{
		if (_currentWindow == window) return;

		var currentWindow = _windows[window];
		_currentWindow = window;

		if (currentWindow.IsOpen == false) currentWindow.Open();

		foreach (var windowTemp in _windows.Values)
		{
			if(windowTemp != currentWindow) windowTemp.Close();
		}
	}

}
