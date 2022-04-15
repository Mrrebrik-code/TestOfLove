using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ColorObject : MonoBehaviour
{

	[SerializeField] private Image _image;
	[SerializeField] private Color[] _colors;
	public int Index = 0;

	private void Awake()
	{
		SaveManager.Subscribe(this);
		GetComponent<Button>().onClick.AddListener(SwitchColor);
	}

	private void SwitchColor()
	{
		Index++;
		if (Index >= _colors.Length) Index = 0;

		SetColor(Index);
	}

	public void SetColor(int index)
	{
		_image.DOColor(_colors[index], 1f);
		Index = index;
	}
}
