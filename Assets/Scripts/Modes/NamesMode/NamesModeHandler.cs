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
		var count = 0f;
		_countGame++;
		if (_countGame % UnityEngine.Random.Range(2,5) == 0)
		{
			count = 1;
		}
		else
		{
			count = UnityEngine.Random.Range(0.1f, 1f);
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
	}
}
