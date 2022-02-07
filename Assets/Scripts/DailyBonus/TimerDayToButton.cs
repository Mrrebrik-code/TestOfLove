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

	private void FixedUpdate()
	{
		if (_isTimer)
		{

		}
	}
	public void SetButtonStatus(string text, bool interactable)
	{
		_buttonText.text = text;
		if (_button.interactable == false && interactable == false)
		{
			return;
		}
		_button.interactable = interactable;
		
	}
	public void SetButton(bool isTimer)
	{
		if (isTimer)
		{
			_isTimer = isTimer;
		}
		else
		{
			_buttonText.text = "Забрать награду";
		}
	}
}
