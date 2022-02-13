using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationText : MonoBehaviour
{
	private enum TypeText
	{
		TMP,
		DEFAULT
	}

	[SerializeField] private string _id;

	private TypeText _type;
	private TMP_Text _tmpText;
	private Text _text;

	private void Start()
	{
		_tmpText = GetComponent<TMP_Text>();
		_text =	GetComponent<Text>();

		if (_tmpText != null)
		{
			_type = TypeText.TMP;
			EditTextTMP();
			Localization.Instance.Subscribe(EditTextTMP);
		} 
		else
		{
			_type = TypeText.DEFAULT;
			EditText();
			Localization.Instance.Subscribe(EditText);
		}
		
	}
	public void EditText()
	{
		Debug.Log(gameObject.name);
		if(_text != null) _text.text = Localization.Instance.Localize(_id);
		
	}
	public void EditTextTMP()
	{
		if (_tmpText != null) _tmpText.text = Localization.Instance.Localize(_id);
	}
}
