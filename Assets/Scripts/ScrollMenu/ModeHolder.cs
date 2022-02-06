using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ModeHolder : MonoBehaviour
{
	public RectTransform RectTransform { get { return _rectTransform; } }
	[SerializeField] private RectTransform _rectTransform;
	[SerializeField] private TMP_Text _nameText;
	[SerializeField] private StatusMode _status;
	[SerializeField] private CategoryButton _categoryButton;
	[SerializeField] private Image _icon;


	public void Init(ScrollObejct scrollObject)
	{
		_nameText.text = scrollObject.NameMode;
		_status.SetStyle(scrollObject.Type);
		_categoryButton.Init(scrollObject.Category);
		_icon.sprite = scrollObject.Icon;
	}
}
