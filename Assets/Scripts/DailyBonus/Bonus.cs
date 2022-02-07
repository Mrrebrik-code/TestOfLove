using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bonus", menuName = "Custom/Bonus")]
public class Bonus : ScriptableObject
{
	[SerializeField] private int _id;
	[SerializeField] private Sprite _icon;
	[SerializeField] private int _countReward;
	[SerializeField] private BonusType _type;
	public int Id { get { return _id; } }
	public Sprite Icon { get { return _icon; } }
	public int CountReward { get { return _countReward; } }
	public BonusType Type { get { return _type; } }
	public bool IsTake 
	{ 
		get 
		{
			if (PlayerPrefs.HasKey($"Bonus_day_{_id}"))
			{
				var isTake = System.Convert.ToBoolean(PlayerPrefs.GetInt($"Bonus_day_{_id}"));
				return isTake;
			}
			else
			{
				return false;
			}
		} 
	}


	public void SaveReward()
	{
		PlayerPrefs.SetInt($"Bonus_day_{_id}", 1);
	}
}
