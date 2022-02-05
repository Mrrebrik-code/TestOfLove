using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SliderHolder : MonoBehaviour
{
	[SerializeField] private TMP_Text _valueSlider;
	[SerializeField] private GameObject _fader;
	private Slider _slider;
	private bool _isActive = true;
	public void SetTextValueSlider()
	{
		if (_slider == null) _slider = GetComponent<Slider>();
		_valueSlider.text = _slider.value.ToString();
	}

	public void SwitchActivitySlider()
	{
		if (_slider == null) _slider = GetComponent<Slider>();
		_isActive = !_isActive;
		_slider.interactable = _isActive;
		_fader.SetActive(!_isActive);
	}
}
