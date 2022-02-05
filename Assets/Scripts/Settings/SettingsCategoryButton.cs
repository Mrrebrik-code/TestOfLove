using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsCategoryButton : MonoBehaviour
{
	[SerializeField] private SettingsHandler.Category _category;
	[SerializeField] private List<Sprite> _sprites = new List<Sprite>();
	[SerializeField] private Image _image;
	private Button _button;
	private void Awake()
	{
		_button = GetComponent<Button>();
		SettingsHandler.Instance.onSetCategory += (category) =>
		{
			if(category == _category) Selected();
			else UnSelected();
		};

		_button.onClick.AddListener(() =>
		{
			SettingsHandler.Instance.SetCategory(_category);
		});
	}
	private void Selected()
	{
		_image.sprite = _sprites[0];
	}

	private void UnSelected()
	{
		_image.sprite = _sprites[1];
	}
}
