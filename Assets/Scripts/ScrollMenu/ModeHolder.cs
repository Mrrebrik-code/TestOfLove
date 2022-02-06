﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ModeHolder : MonoBehaviour
{
	public RectTransform RectTransform { get { return _rectTransform; } }
	[SerializeField] private RectTransform _rectTransform;
	[SerializeField] private TMP_Text _nameText;
	[SerializeField] private StatusMode _status;
	[SerializeField] private CategoryButton _categoryButton;
	[SerializeField] private Image _icon;

	[SerializeField] private GameObject[] _objectsToIcon;


	public void Init(ScrollObejct scrollObject)
	{
		switch (scrollObject.TypeMode)
		{
			case TypeMode.VIPTestNames:
				_objectsToIcon[0].SetActive(true);
				_objectsToIcon[1].SetActive(false);
				break;
			case TypeMode.VIPNamesToTree:
				_objectsToIcon[0].SetActive(false);
				_objectsToIcon[1].SetActive(true);
				break;
		}
		_nameText.text = scrollObject.NameMode;
		_status.SetStyle(scrollObject.Type);
		_categoryButton.Init(scrollObject.Category);
		_icon.sprite = scrollObject.Icon;
	}
}