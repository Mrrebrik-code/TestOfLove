using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : SingletonMono<SettingsHandler>
{
	public enum Category
	{
		Audio,
		Languages
	}

	[SerializeField] private Slider _sliderSound;
	[SerializeField] private Slider _sliderMusic;
	[SerializeField] private float _defaultValueSound;
	[SerializeField] private float _defaultValueMusic;
	private float _valueSound;
	private float _valueMusic;

	public Action<SettingsHandler.Category> onSetCategory;
	public SettingsHandler.Category CurrentCategory { get; private set; }
	[SerializeField] private List<SettingsCategory> _categorys;

	public override void Awake()
	{
		base.Awake();
		Init();
	}

	private void Init()
	{
		if (PlayerPrefs.HasKey("Value_Sound")) _valueSound = PlayerPrefs.GetFloat("Value_Sound");
		else
		{
			_valueSound = _defaultValueSound;
			PlayerPrefs.SetFloat("Value_Sound", _defaultValueSound);
		}

		if (PlayerPrefs.HasKey("Value_Music")) _valueMusic = PlayerPrefs.GetFloat("Value_Music");
		else
		{
			_valueMusic = _defaultValueMusic;
			PlayerPrefs.SetFloat("Value_Music", _defaultValueMusic);
		}

		_sliderSound.value = _valueSound;
		_sliderMusic.value = _valueMusic;

		AudioManager.Instance.SetValues(_valueSound, _valueMusic);
	}

	public void ChangedValueSound()
	{
		_valueSound = _sliderSound.value;
		PlayerPrefs.SetFloat("Value_Sound", _valueSound);
		AudioManager.Instance.SetValues(_valueSound, _valueMusic);
	}

	public void ChangedValueMusic()
	{
		_valueMusic = _sliderMusic.value;
		PlayerPrefs.SetFloat("Value_Music", _valueMusic);
		AudioManager.Instance.SetValues(_valueSound, _valueMusic);
	}


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
