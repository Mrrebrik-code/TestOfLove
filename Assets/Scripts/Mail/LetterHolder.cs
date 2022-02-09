using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LetterHolder : MonoBehaviour
{
	public event Action<Letter, Action> onReadLetter;
	public event Action<Letter, Action> onDeleteLetter;
	private Letter _letter;
	[SerializeField] private TMP_Text _tittleText;
	[SerializeField] private TMP_Text _temaText;
	[SerializeField] private TMP_Text _descriptionText;
	[SerializeField] private Image _icon;
	[SerializeField] private GameObject _lenta;
	[SerializeField] private TMP_Text _dateText;
	[SerializeField] private Image _image;
	[SerializeField] private Color[] _colorsPanel;
	public void Init(Letter letter)
	{
		_letter = letter;
		_tittleText.text = Localization.Instance.Localize(_letter.Tittle);
		_temaText.text = Localization.Instance.Localize(_letter.Tema);
		_descriptionText.text = Localization.Instance.Localize((_letter.Description));
		Localization.Instance.Subscribe(() =>
		{
			_tittleText.text = Localization.Instance.Localize(_letter.Tittle);
			_temaText.text = Localization.Instance.Localize(_letter.Tema);
			_descriptionText.text = Localization.Instance.Localize((_letter.Description));
		});

		if (letter.IsView)
		{
			_image.color = _colorsPanel[1];
		}
		else
		{
			_image.color = _colorsPanel[0];
		}
		
		_icon.sprite = letter.Icon;
		_lenta.SetActive(letter.IsLenta);
		_dateText.text = letter.Data.ToShortDateString();
	}

	public void Read()
	{
		onReadLetter?.Invoke(_letter, () =>
		{
			_image.color = _colorsPanel[1];
			GetComponent<RectTransform>().SetAsLastSibling();
		});
	}

	public void Delete()
	{
		onDeleteLetter?.Invoke(_letter, () =>
		{
			DestroyImmediate(gameObject);
		});
	}
}
