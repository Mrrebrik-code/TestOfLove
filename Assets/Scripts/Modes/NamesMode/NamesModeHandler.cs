using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class NamesModeHandler : MonoBehaviour
{
	[SerializeField] private Image _fill;
	[SerializeField] private TMP_Text _countText;
	[SerializeField] private TMP_InputField _inputName1;
	[SerializeField] private TMP_InputField _inputName2;
	private Coroutine _coroutine;
	private int _countGame = 0;

	public void Test()
	{
		if (string.IsNullOrEmpty(_inputName1.text) || string.IsNullOrEmpty(_inputName2.text)) return;


		var count = 0f;

		if (PlayerPrefs.HasKey($"{_inputName1.text}_{_inputName2.text}"))
		{
			count = PlayerPrefs.GetFloat($"{_inputName1.text}_{_inputName2.text}");
		}
		else
		{
			_countGame++;
			if (_countGame % UnityEngine.Random.Range(2, 5) == 0)
			{
				count = 1;
			}
			else
			{
				count = UnityEngine.Random.Range(0.1f, 1f);
			}
			PlayerPrefs.SetFloat($"{_inputName1.text}_{_inputName2.text}", count);
		}
		

		if (_coroutine != null) StopCoroutine(_coroutine);
		_coroutine = StartCoroutine(Delay(count));
		_fill.DOFillAmount(count, 2f);
	}

	private IEnumerator Delay(float count)
	{
		var progression = Convert.ToInt32(count * 100);
		if (progression == 99) progression = 100;
		for (int i = 0; i < progression; i++)
		{
			_countText.text = $"{i}%";
			yield return new WaitForSeconds(0.01f);
		}

		MailManager.Instance.AddLetter(Letters.CompletTestOfNamesMode, false);
	}
}
