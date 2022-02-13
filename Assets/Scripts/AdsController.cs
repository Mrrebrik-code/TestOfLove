using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdsController : MonoBehaviour
{
	public static AdsController Instance;
	[SerializeField] private float _time;
	[SerializeField] private float _workTime;
	[SerializeField] private bool _isInterstitial = false;

	private void Start()
	{
		if(Instance == null) Instance = this;
		else Destroy(gameObject);

		_workTime = _time;
	}

	private void Update()
	{
		Timer();
	}


	public void ShowInterstitial()
	{
		if (_isInterstitial)
		{
			_isInterstitial = false;
			if (DeviceManager.Instance._isUnity)
			{
				Debug.Log("ShowInterstitial");
			}
			else
			{
				YandexSDK.Instance.ShowInterstitial();
			}
			
		}
	}



	private void Timer()
	{
		_workTime -= Time.deltaTime;
		if (_workTime <= 0)
		{
			_workTime = _time;
			_isInterstitial = true;
			ShowInterstitial();
		}
	}
}
