using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class ScanerModeHandler : SingletonMono<ScanerModeHandler>
{
	[SerializeField] private List<FingerprintHolder> _fingerprintHolders = new List<FingerprintHolder>();
	[SerializeField] private TMP_Text _message;
	private bool _isStartScaning = false;
	[SerializeField] private Slider _progression;
	[SerializeField] private TMP_Text _valueProgression;
	private bool _isComplet = false;
	[SerializeField] private Button _buttonScanning;
	[SerializeField] private ResultScaner _result;
	

	private void Start()
	{
		SetMessage("core_002");
	}

	public void SetMessage(string id)
	{
		StartCoroutine(Delay(id));
	}

	private IEnumerator Delay(string id)
	{
		_message.DOFade(0, 1f);
		yield return new WaitForSeconds(1f);
		if (id == "") _message.text = id;
		else
		{
			_message.text = Localization.Instance.Localize(id);
			_message.DOFade(1, 0.8f);
		}
		
		
		
	}
	public void Scaning()
	{
		_buttonScanning.interactable = false;
		foreach (var finger in _fingerprintHolders)
		{
			finger.Init();
			finger.onScanningError += HandleError;
		}
	}

	private void HandleError()
	{
		SetMessage("core_004");
		foreach (var finger in _fingerprintHolders)
		{
			if(_isStartScaning)
			{

				finger.ResetScaner();
			}
			
		}
		_isStartScaning = false;
	}

	public void StartProgression()
	{
		if(_isComplet == false)
		{
			_isComplet = true;
			_progression.onValueChanged.AddListener((value) =>
			{
				_valueProgression.text = $"{value * 100}%";
				if(value == 1)
				{
					_isComplet = false;
					_isStartScaning = false;
					_buttonScanning.interactable = true;
					foreach (var finger in _fingerprintHolders)
					{
						finger.onScanningError -= HandleError;
					}
					_progression.value = 0;

					_result.Init();
					SetMessage("core_002");
				}
			});
			_progression.DOValue(1, 8f);

		}
		
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
				SetMessage("core_003");
			}
		}
		
	}

}
