using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationButton : MonoBehaviour
{
	[SerializeField] private Languages _language;
	[SerializeField] private Outline _outline;
	private Button _button;

	private void Start()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(() =>
		{
			Localization.Instance.SetLanguage(_language.ToString());
			//_outline.enabled = true;
		});
		Localization.Instance.Subscribe(OutlineSwitching);
		OutlineSwitching();
	}

	private void OutlineSwitching()
	{
		if (_language.ToString() == Localization.Instance.Language.ToString()) _outline.enabled = true;
		else _outline.enabled = false;
	}

	private void OnDestroy()
	{
		Localization.Instance.UnSubscribe(OutlineSwitching);
	}
}
