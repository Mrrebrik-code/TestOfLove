using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusMode : MonoBehaviour
{
	[SerializeField] private List<Style> _styles = new List<Style>();
	[SerializeField] private Image _image;
	[SerializeField] private TMP_Text _tittleText;
	private bool _isLocalization = false;
	private string _idTittle;
	public void SetStyle(Style.Type type)
	{
		_styles.ForEach(style =>
		{
			if(style.TypeStyle == type)
			{
				
				if (_isLocalization == false)
				{
					_isLocalization = true;
					Localization.Instance.Subscribe(LocalizationTextx);
				}

				_image.sprite = style.Sprite;
				_tittleText.text = Localization.Instance.Localize(style.Tittle);
				_idTittle = style.Tittle;
			}
		});
	}
	private void LocalizationTextx()
	{
		_tittleText.text = Localization.Instance.Localize(_idTittle);
	}

	[Serializable]
	public class Style
	{
		public enum Type
		{
			Red,
			Green,
			Yellow
		}
		public Type TypeStyle;
		public Sprite Sprite;
		public string Tittle;
	}
}
