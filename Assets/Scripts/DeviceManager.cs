using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : SingletonMono<DeviceManager>
{
	[SerializeField] private bool _isUnity = true;
	[SerializeField] private bool _isDesktop = true;
	[SerializeField] private GameObject _textInfoDevice;
	public Devices Type { get; private set; }

	public override void Awake()
	{
		base.Awake();
		if (_isUnity)
		{
			if (_isDesktop)
			{
				Type = Devices.Desktop;
			}
			else
			{
				Type = Devices.Mobile;
			}
			if (Type == Devices.Desktop)
			{
				_textInfoDevice.SetActive(true);
			}
		}
		else
		{
			YandexSDK.Instance.onDeviceInfo += HandleDeviceCurrent;
			YandexSDK.Instance.GetInfoDevice();
		}
		
	}

	private void HandleDeviceCurrent(string device)
	{
		switch (device)
		{
			case "desktop":
				Type = Devices.Desktop;
				break;
			case "mobile":
				Type = Devices.Mobile;
				break;
			default:
				Type = Devices.Desktop;
				break;
		}

		if(Type == Devices.Desktop)
		{
			_textInfoDevice.SetActive(true);
		}
	}
}
