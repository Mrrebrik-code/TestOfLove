using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LetterHolder : MonoBehaviour
{
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
		_tittleText.text = letter.Tittle;
		_temaText.text = letter.Tema;
		_descriptionText.text = letter.Description;
		_icon.sprite = letter.Icon;
		_lenta.SetActive(letter.IsLenta);
		_dateText.text = letter.Data.ToShortDateString();
	}
}
