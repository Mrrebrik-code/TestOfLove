using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Letter", menuName = "Custom/Letter")]
public class Letter : ScriptableObject
{
	[SerializeField] private int _id;
	[SerializeField] private Letters _type;
	[SerializeField] private string _tittle;
	[SerializeField] private string _tema;
	[SerializeField] private string _discription;
	[SerializeField] private string _fullDescription;
	[SerializeField] private Sprite _icon;
	[SerializeField] private bool _isLenta;
	[SerializeField, Range(0, 1)] private int _isCreatingStart;
	[SerializeField] private TypeLetter _typeLetter;
	public string PromoCode;
	public int CountRewardHeart;
	
 	public bool IsView 
	{ 
		get 
		{
			if (PlayerPrefs.HasKey($"View_{_tittle}_{_id}") == false)
			{
				PlayerPrefs.SetInt($"View_{_tittle}_{_id}", 0);
			}
			return Convert.ToBoolean(PlayerPrefs.GetInt($"View_{_tittle}_{_id}"));
		}
		set
		{
			PlayerPrefs.SetInt($"View_{_tittle}_{_id}", Convert.ToInt32(value));
		}
	}

	public bool IsTakeReward
	{
		get
		{
			if (_typeLetter == TypeLetter.Default || _typeLetter == TypeLetter.RewardPromoCode) return true;

			if (PlayerPrefs.HasKey($"TakeReward_{_tittle}_{_id}") == false)
			{
				PlayerPrefs.SetInt($"TakeReward_{_tittle}_{_id}", 0);
			}
			return Convert.ToBoolean(PlayerPrefs.GetInt($"TakeReward_{_tittle}_{_id}"));
		}
		set
		{
			PlayerPrefs.SetInt($"TakeReward_{_tittle}_{_id}", Convert.ToInt32(value));
		}
	}

	public bool IsDelete
	{
		get
		{
			if (PlayerPrefs.HasKey($"Delete_{_tittle}_{_id}") == false)
			{
				PlayerPrefs.SetInt($"Delete_{_tittle}_{_id}", 0);
			}
			return Convert.ToBoolean(PlayerPrefs.GetInt($"Delete_{_tittle}_{_id}"));
		}
		set
		{
			PlayerPrefs.SetInt($"Delete_{_tittle}_{_id}", Convert.ToInt32(value));
		}
	}
	public bool IsCreating
	{
		get
		{
			if (PlayerPrefs.HasKey($"Creating{_tittle}_{_id}") == false)
			{
				PlayerPrefs.SetInt($"Creating{_tittle}_{_id}", _isCreatingStart);
			}
			return Convert.ToBoolean(PlayerPrefs.GetInt($"Creating{_tittle}_{_id}"));
		}
		set
		{
			PlayerPrefs.SetInt($"Creating{_tittle}_{_id}", Convert.ToInt32(value));
		}
	}
	public DateTime Data 
	{ 
		get 
		{
			if(PlayerPrefs.HasKey($"Date_{_tittle}_{_id}") == false)
			{
				PlayerPrefs.SetString($"Date_{_tittle}_{_id}", DateTime.Now.ToShortDateString());
			}
			return Convert.ToDateTime(PlayerPrefs.GetString($"Date_{_tittle}_{_id}")); 
		}
		set
		{
			PlayerPrefs.SetString($"Date_{_tittle}_{_id}", value.ToShortDateString());
		}
	}

	public Letters Type { get { return _type; } }
	public string Tittle { get { return _tittle; } }
	public string Tema { get { return _tema; } }
	public string Description { get { return _discription; } }
	public string FullDescription { get { return _fullDescription; } }
	public Sprite Icon { get { return _icon; } }
	public bool IsLenta { get { return _isLenta; } }
	public TypeLetter TypeLetter { get { return _typeLetter; } }

}
