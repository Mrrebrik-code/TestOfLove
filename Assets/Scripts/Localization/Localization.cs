using UnityEngine;
using Assets.SimpleLocalization;
using System;
using System.Collections.Generic;

public class Localization : SingletonMono<Localization>
{
	public Languages Language;
	public override void Awake()
	{
		base.Awake();
		Init();
	}
	private void Init()
	{
		LocalizationManager.Read();
		if (PlayerPrefs.HasKey("CurrentLanguage"))
		{
			var language = PlayerPrefs.GetString("CurrentLanguage");
			Instance.SetLanguage(language);
		}
		else
		{
			Instance.SetLanguage(Languages.Russian.ToString());
		}
	}
	public void SetLanguage(string language)
	{
		switch (language)
		{
			case "English": Language = Languages.English; break;
			case "Turkish": Language = Languages.Turkish; break;
			case "Russian": Language = Languages.Russian; break;
			case "French": Language = Languages.French; break;
			case "Italian": Language = Languages.Italian; break;
			case "Ukrainian": Language = Languages.Ukrainian; break;
			case "Moldavian": Language = Languages.Moldavian; break;
			case "Deutsch": Language = Languages.Deutsch; break;
		}
		LocalizationManager.Language = language;
		PlayerPrefs.SetString("CurrentLanguage", Language.ToString());
	}

	public void Subscribe(Action callback)
	{
		LocalizationManager.LocalizationChanged += callback;
	}
	public void UnSubscribe(Action callback)
	{
		LocalizationManager.LocalizationChanged -= callback;
	}


	public string Localize(string id)
	{
		return LocalizationManager.Localize($"{id}");
	}
}
