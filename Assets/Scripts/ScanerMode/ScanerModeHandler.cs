using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanerModeHandler : MonoBehaviour
{
	[SerializeField] private List<FingerprintHolder> _fingerprintHolders = new List<FingerprintHolder>();
	private bool _isStartScaning = false;
	public void Scaning()
	{
		foreach (var finger in _fingerprintHolders)
		{
			finger.Init();
			finger.onScanningError += HandleError;
		}
	}

	private void HandleError()
	{
		foreach (var finger in _fingerprintHolders)
		{
			if(_isStartScaning)
			{

				finger.ResetScaner();
			}
			
		}
		_isStartScaning = false;
	}

	private void Update()
	{
		if(_isStartScaning == false)
		{
			
			if (_fingerprintHolders[0].IsStartScaning && _fingerprintHolders[1].IsStartScaning)
			{
				_isStartScaning = true;
				foreach (var finger in _fingerprintHolders)
				{
					finger.Scaning();
				}
			}
		}
		
	}

}
