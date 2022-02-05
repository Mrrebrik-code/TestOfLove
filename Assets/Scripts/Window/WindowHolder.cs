using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WindowHolder : MonoBehaviour
{
	[SerializeField] private Window _window;
	public Window Window { get { return _window; } }
	public Action<Window> onOpenWindow { get; set; }

	private bool _isInit = false;
	public bool IsOpen { get { return gameObject.activeInHierarchy; } }
	private void Start()
	{
		Init();
		onOpenWindow?.Invoke(_window);
	}

	public void OnEnable()
	{
		onOpenWindow?.Invoke(_window);
	}

	private void Init()
	{
		if (_isInit == false)
		{
			_isInit = true;
			WindowManager.Instance.SubscribeWindow(this);
		}
	}
	public void Close()
	{
		gameObject.SetActive(false);
	}
	public void Open()
	{
		gameObject.SetActive(true);
	}
}
