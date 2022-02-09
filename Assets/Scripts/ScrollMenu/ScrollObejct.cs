using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScrollObject", menuName = "Custom/ScrollObejct")]
public partial class ScrollObejct : ScriptableObject
{
	[SerializeField] private int _id;
	[SerializeField] private TypeMode _typeMode;
	[SerializeField] private string _nameMode;
	[SerializeField] private Sprite _icon;
	[SerializeField] private Categorys _category;
	[SerializeField] private ModeHolder _modePrefab;
	[SerializeField] private bool _isLock;
	private StatusMode.Style.Type _type;
	
	

	public int Id { get { return _id; } }
	public string NameMode { get { return _nameMode; } }
	public Sprite Icon { get { return _icon;} }
	public Categorys Category { get { return _category; } }
	public ModeHolder ModePrefab { get { return _modePrefab;} }
	public bool IsLock 
	{ 
		get 
		{ 
			return Convert.ToBoolean(PlayerPrefs.GetInt($"Category_{_id}_{_nameMode}_locker", Convert.ToInt32(_isLock)));
		}
		set
		{
			PlayerPrefs.SetInt($"Category_{_id}_{_nameMode}_locker", Convert.ToInt32(value));
		}
	}

	public bool IsComplet
	{
		get
		{
			return Convert.ToBoolean(PlayerPrefs.GetInt($"Category_{_id}_{_nameMode}_complet", Convert.ToInt32(0)));
		}
		set
		{
			PlayerPrefs.SetInt($"Category_{_id}_{_nameMode}_complet", Convert.ToInt32(value));
		}
	}
	public StatusMode.Style.Type Type 
	{ 
		get 
		{
			string type = PlayerPrefs.GetString($"Category_{_id}_{_nameMode}", StatusMode.Style.Type.Green.ToString());

			switch (type)
			{
				case "Red":
					_type = StatusMode.Style.Type.Red;
					break;
				case "Green":
					_type = StatusMode.Style.Type.Green;
					break;
				case "Yellow":
					_type = StatusMode.Style.Type.Yellow;
					break;
			}
			if (_isLock)
			{
				return StatusMode.Style.Type.Red;
			}
			return _type; 
		}
		set
		{
			PlayerPrefs.SetString($"Category_{_id}_{_nameMode}", value.ToString());
		}
	}
	public TypeMode TypeMode { get { return _typeMode;} }

	public void Locker(bool isLock)
	{
		_isLock = isLock;
		Type = StatusMode.Style.Type.Red;
	}
}
