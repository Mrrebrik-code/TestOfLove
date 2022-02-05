﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
	[SerializeField] private Categorys _category;
	private Button _button;

	private void Start()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(() =>
		{
			var successful = GameManager.SetCategory(_category);
			if (successful) SceneLoader.Instance.Load("_Game");
		});
	}
}