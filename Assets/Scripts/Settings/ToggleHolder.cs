using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itibsoft.TOAnimate;
using UnityEngine.UI;
using TMPro;
using System;

public class ToggleHolder : MonoBehaviour
{
	[SerializeField] private Vector2 _positions;
	[SerializeField] private Sprite[] _sprites;
	[SerializeField] private Image _handle;
	[SerializeField] private TMP_Text _statusText;
	[SerializeField] private SliderHolder _slider;
	private bool _isActive = false;

	private void Start()
	{
		_slider.onActiveSldier += HandleActiveSlider;
		Localization.Instance.Subscribe(() =>
		{
			_statusText.text = _isActive == true ? "ÂÊË" : "ÂÛÊË";
		});
	}

	private void HandleActiveSlider(bool arg1, string arg2)
	{
	}

	public void SwitchToggle(bool isSlider = true)
	{
		_isActive = !_isActive;
		var position = new Vector2(_isActive == true ? _positions.x : _positions.y, _handle.rectTransform.anchoredPosition.y);
		_handle.rectTransform.TOAMove(position, 2f, () =>
		{
			_statusText.text = _isActive == true ? "ÂÊË" : "ÂÛÊË";
			_handle.sprite = _isActive == true ? _sprites[0] : _sprites[1];
		});
		if (isSlider)
		{
			_slider.SwitchActivitySlider();
		}
		
	}
}
