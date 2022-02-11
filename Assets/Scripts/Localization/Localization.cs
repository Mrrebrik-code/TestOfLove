using UnityEngine;
//using Assets.SimpleLocalization;
using System;
using System.Collections.Generic;

public class Localization : SingletonMono<Localization>
{
	public event Action onLocalizationChanged;
	[SerializeField] private Languages _language;
	public Languages Language { get { return _language; } set { _language = value; onLocalizationChanged?.Invoke(); } }
	public List<LocalizationObject> LocalizationList = new List<LocalizationObject>();
	public override void Awake()
	{
		base.Awake();
		YandexSDK.Instance.onLanguagesCurrentToDomen += (language) => Language = language;
		Init();
	}

	private void Init()
	{
		//LocalizationManager.Read();
		if (PlayerPrefs.HasKey("CurrentLanguage"))
		{
			var language = PlayerPrefs.GetString("CurrentLanguage");
			Instance.SetLanguage(language);
		}
		else
		{
			YandexSDK.Instance.GetLanguage();
			//Instance.SetLanguage(Languages.Russian.ToString());
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
		//LocalizationManager.Language = language;
		PlayerPrefs.SetString("CurrentLanguage", Language.ToString());
	}

	public void Subscribe(Action callback)
	{
		onLocalizationChanged += callback;
		//LocalizationManager.LocalizationChanged += callback;
	}
	public void UnSubscribe(Action callback)
	{
		onLocalizationChanged -= callback;
		//LocalizationManager.LocalizationChanged -= callback;
	}


	public string Localize(string id)
	{
		foreach (var localization in LocalizationList)
		{
			if(localization.ID == id)
			{
				switch (Language)
				{
					case Languages.Russian: return localization.Groups.RU;
					case Languages.English: return localization.Groups.EN;
					case Languages.Turkish: return localization.Groups.TR;

				}
			}
		}
		return "NULL"; //LocalizationManager.Localize($"{id}");
	}

	public string GetParameter(string id, string massa = "Massa")
	{
		foreach (var localization in LocalizationList)
		{
			if (localization.ID == id)
			{
				return localization.Groups.MASSA;
			}
		}
		return "NULL";/*LocalizationManager.GetParam(id, massa);*/
	}

	[Serializable]
	public class LocalizationObject
	{
		public string ID;
		public Group Groups;
		public LocalizationObject(string id, Group group)
		{
			ID = id;
			Groups = group;
		}

		[Serializable]
		public class Group
		{
			public string RU;
			public string EN;
			public string TR;
			public string MASSA;

			public Group(string ru, string en, string tr, string massa)
			{
				RU = ru;
				EN = en;

				TR = tr;
				MASSA = massa;
			}
		}
	}
}
