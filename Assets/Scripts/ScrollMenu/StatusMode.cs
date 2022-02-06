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
	public void SetStyle(Style.Type type)
	{
		_styles.ForEach(style =>
		{
			if(style.TypeStyle == type)
			{
				_image.sprite = style.Sprite;
				_tittleText.text = style.Tittle;
			}
		});
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
