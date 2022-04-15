using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonLoad : MonoBehaviour
{
	[SerializeField] private TMP_Text _textInfo;
	[SerializeField] private TMP_Text _textInfoData;
	private void Awake()
	{
		YandexSDK.Instance.onLoadData += HandleLoad;
		var button = GetComponent<Button>();
		button.onClick.AddListener(() =>
		{
			_textInfoData.text = "";
			SaveManager.Load();
		});
	}

	private void HandleLoad(string obj)
	{
		_textInfo.text = "ЗАГРУЖЕНЫ ДАННЫЕ";
		_textInfoData.text += obj;
	}
}
