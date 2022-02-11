using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerDayToButton : MonoBehaviour
{
	[SerializeField] private Button _button;
	[SerializeField] private TMP_Text _buttonText;
	private bool _isTimer = false;
	private string _idLocalization;
	private bool _isLocalization = false;
	public void SetButtonStatus(string text, bool interactable)
	{
		_idLocalization = text;
		if(_idLocalization == "core_055")
		{
			_buttonText.text = Localization.Instance.Localize(_idLocalization);
		}
		else
		{
			_buttonText.text = text;
		}
		
		if(_isLocalization == false)
		{
			_isLocalization = true;
			Localization.Instance.Subscribe(LocalizationText);
		}
		
		if (_button.interactable == false && interactable == false)
		{
			return;
		}
		_button.interactable = interactable;
	}

	private void LocalizationText()
	{
		if(_idLocalization == "core_055") _buttonText.text = Localization.Instance.Localize(_idLocalization);
	}
}
