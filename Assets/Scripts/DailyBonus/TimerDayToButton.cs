using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerDayToButton : MonoBehaviour
{
	[SerializeField] private TMP_Text _buttonText;
	private bool _isTimer = false;

	private void FixedUpdate()
	{
		if (_isTimer)
		{

		}
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
