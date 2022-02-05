using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationButton : MonoBehaviour
{
	[SerializeField] private Languages _language;
	[SerializeField] private List<Sprite> _outlineSprites;
	[SerializeField] private Image _icon;
	private Button _button;

	private void Start()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(() =>
		{
			Localization.Instance.SetLanguage(_language.ToString());
		});
		Localization.Instance.Subscribe(OutlineSwitching);
		OutlineSwitching();
	}

	private void OutlineSwitching()
	{
		if (_language.ToString() == Localization.Instance.Language.ToString()) _icon.sprite = _outlineSprites[0];
		else _icon.sprite = _outlineSprites[1];
	}

	private void OnDestroy()
	{
		Localization.Instance.UnSubscribe(OutlineSwitching);
	}
}
