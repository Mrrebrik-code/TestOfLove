using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SliderHolder : MonoBehaviour
{
	public event Action<bool, string> onActiveSldier;
	[SerializeField] private string _type;
	[SerializeField] private TMP_Text _valueSlider;
	[SerializeField] private GameObject _fader;
	private Slider _slider;
	public bool IsActive = true;
	private void Start()
	{
		if(PlayerPrefs.HasKey($"Active_status_slider_{_type}"))
		{
			IsActive = System.Convert.ToBoolean(PlayerPrefs.GetInt($"Active_status_slider_{_type}"));
		}
		else
		{
			IsActive = true;
			PlayerPrefs.SetInt($"Active_status_slider_{_type}", System.Convert.ToInt32(IsActive));
		}

		if(IsActive == false)
		{
			_slider.interactable = IsActive;
			_fader.SetActive(!IsActive);
			onActiveSldier?.Invoke(IsActive, _type);
		}
	}
	public void SetTextValueSlider()
	{
		if (_slider == null) _slider = GetComponent<Slider>();
		_valueSlider.text = _slider.value.ToString();
	}

	public void SwitchActivitySlider(bool isToogle = true)
	{
		if (_slider == null) _slider = GetComponent<Slider>();
		IsActive = !IsActive;
		_slider.interactable = IsActive;
		_fader.SetActive(!IsActive);

		PlayerPrefs.SetInt($"Active_status_slider_{_type}",  System.Convert.ToInt32(IsActive));
		
	}
}
