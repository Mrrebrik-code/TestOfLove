using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerprintHolder : MonoBehaviour
{
	public Action onScanningError;
	[SerializeField] private Animator _animator;
	private bool _isScanerOn = false;
	[SerializeField] private bool _isPressed = false;
	public bool IsStartScaning = false;
	public void Init()
	{

		_animator.SetTrigger("ScanerOn");
	}

	public void On()
	{
		_isScanerOn = true;
		ScanerModeHandler.Instance.SetMessage("core_001");
		Debug.LogError("_isScanerOn");
	}

	private void Update()
	{
		if(_isPressed == true && IsStartScaning == false)
		{
			IsStartScaning = true;
		}
	}

	public void Scaning()
	{
		_animator.SetTrigger("LineOn");
	}

	public void Complet()
	{
		ScanerModeHandler.Instance.SetMessage("core_005");
		_isPressed = false;
		_isScanerOn = false;
		IsStartScaning = false;
		ScanerModeHandler.Instance.StartProgression();
	}

	public void Pressed(bool isPressed)
	{
		if (_isScanerOn)
		{
			_isPressed = isPressed;
		}
		if(IsStartScaning == true && isPressed == false)
		{
			onScanningError?.Invoke();
			IsStartScaning = false;
		}
		
	}

	public void ResetScaner()
	{
		_isPressed = false;
		IsStartScaning = false;
		_animator.SetTrigger("Reset");
	}




}
