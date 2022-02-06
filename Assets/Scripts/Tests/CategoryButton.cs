﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
	private Categorys _category;
	private string _sceneName;
	private Button _button;

	public void Init(Categorys category)
	{
		_category = category;
		_button = GetComponent<Button>();
		_button.onClick.AddListener(() =>
		{
			var successful = GameManager.SetCategory(_category);
			if (successful) SceneLoader.Instance.Load("_Game");
		});
	}
	public void Init(TypeMode type, string nameScene)
	{
		_sceneName = nameScene;
		_button = GetComponent<Button>();
		_button.onClick.AddListener(() =>
		{

			SceneLoader.Instance.Load(_sceneName);
		});
	}
}
