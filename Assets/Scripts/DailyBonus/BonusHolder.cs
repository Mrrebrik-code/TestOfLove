using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonusHolder : MonoBehaviour
{
	public Bonus Bonus { get; private set; }
	[SerializeField] private Image _imageBody;
	[SerializeField] private Image _icon;
	[SerializeField] private TMP_Text _dayText;
	[SerializeField] private TMP_Text _countText;
	[SerializeField] private GameObject _checkTake;
	private bool _isTake = false;
	[SerializeField] private Style _style;
	public void Init(Bonus bonus)
	{
		Bonus = bonus;
		_icon.sprite = bonus.Icon;
		_dayText.text = $"День {bonus.Id}";
		_countText.text = bonus.CountReward.ToString();

		if (bonus.IsTake)
		{
			_checkTake.SetActive(true);
			_isTake = true;
			SetStyle(Style.Type.Take);
		}
	}
	public void SetStyle(Style.Type type)
	{
		_imageBody.sprite = _style.GetStyle(type);
	}
	public void Take()
	{
		_isTake = true;
		_checkTake.SetActive(true);
		SetStyle(Style.Type.Take);
		Bonus.SaveReward(1);
	}
	public void ResetBonus()
	{
		_checkTake.SetActive(false);
		_isTake = false;
		Bonus.SaveReward(0);
		_imageBody.sprite = _style.GetStyle(Style.Type.Default);
	}

	[System.Serializable]
	public class Style
	{
		public enum Type
		{
			Default,
			Current,
			Take
		}
		[SerializeField] private Sprite[] _sprites;

		public Sprite GetStyle(Type type)
		{
			switch (type)
			{
				case Type.Default: return _sprites[0];
				case Type.Current: return _sprites[1];
				case Type.Take: return _sprites[2];
			}
			return null;
		}
	}
}
