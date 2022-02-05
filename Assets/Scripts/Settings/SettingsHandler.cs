using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler : SingletonMono<SettingsHandler>
{
	public enum Category
	{
		Audio,
		Languages
	}

	public Action<SettingsHandler.Category> onSetCategory;
	public SettingsHandler.Category CurrentCategory { get; private set; }
	[SerializeField] private List<SettingsCategory> _categorys;

	public void SetCategory(SettingsHandler.Category category)
	{
		CurrentCategory = category;
		foreach (var categoryTemp in _categorys)
		{
			if (categoryTemp.Category == CurrentCategory) categoryTemp.Open();
			else categoryTemp.Close();
		}
		onSetCategory?.Invoke(CurrentCategory);
	}
}
