using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LetterHolder : MonoBehaviour
{
	public event Action<Letter> onReadLetter;
	private Letter _letter;
	[SerializeField] private TMP_Text _tittleText;
	[SerializeField] private TMP_Text _temaText;
	[SerializeField] private TMP_Text _descriptionText;
	[SerializeField] private Image _icon;
	[SerializeField] private GameObject _lenta;
	[SerializeField] private TMP_Text _dateText;
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
		
		_icon.sprite = letter.Icon;
		_lenta.SetActive(letter.IsLenta);
		_dateText.text = letter.Data.ToShortDateString();
	}

	public void Read()
	{
		onReadLetter?.Invoke(_letter);
	}
}
