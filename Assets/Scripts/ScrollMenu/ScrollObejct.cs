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
	[SerializeField] private int _priceUnLock;
	
	

	public int Id { get { return _id; } }
	public string NameMode { get { return _nameMode; } }
	public Sprite Icon { get { return _icon;} }
	public Categorys Category { get { return _category; } }
	public ModeHolder ModePrefab { get { return _modePrefab;} }

	public int Price { get { return _priceUnLock; } }
	public bool IsLock 
	{ 
		get 
		{ 
			if(_typeMode == TypeMode.Default)
			{
				return Convert.ToBoolean((int)SaveManager.Categorys.Get($"{_category}", CategorySave.Type.Lock));
				//return Convert.ToBoolean(PlayerPrefs.GetInt($"Category_{_id}_{_nameMode}_locker", Convert.ToInt32(_isLock)));
			}
			else
			{
				return Convert.ToBoolean((int)SaveManager.Categorys.Get($"{_typeMode}", CategorySave.Type.Lock));
			}
			
		}
		set
		{
			if (_typeMode == TypeMode.Default)
			{
				SaveManager.Categorys.Set($"{_category}", Convert.ToInt32(value), CategorySave.Type.Lock);
				SaveManager.Save(SaveManager.TypeData.Category);
			}
			else
			{
				SaveManager.Categorys.Set($"{_typeMode}", Convert.ToInt32(value), CategorySave.Type.Lock);
				SaveManager.Save(SaveManager.TypeData.Category);
			}
			//PlayerPrefs.SetInt($"Category_{_id}_{_nameMode}_locker", Convert.ToInt32(value));
		}
	}

	public bool IsComplet
	{
		get
		{
			if (_typeMode == TypeMode.Default)
			{
				return Convert.ToBoolean((int)SaveManager.Categorys.Get($"{_category}", CategorySave.Type.Complet));
				//return Convert.ToBoolean(PlayerPrefs.GetInt($"Category_{_id}_{_nameMode}_locker", Convert.ToInt32(_isLock)));
			}
			else
			{
				return Convert.ToBoolean((int)SaveManager.Categorys.Get($"{_typeMode}", CategorySave.Type.Complet));
			}
			//return Convert.ToBoolean(PlayerPrefs.GetInt($"Category_{_id}_{_nameMode}_complet", Convert.ToInt32(0)));
		}
		set
		{
			if (_typeMode == TypeMode.Default)
			{
				SaveManager.Categorys.Set($"{_category}", Convert.ToInt32(value), CategorySave.Type.Complet);
				SaveManager.Save(SaveManager.TypeData.Category);
			}
			else
			{
				SaveManager.Categorys.Set($"{_typeMode}", Convert.ToInt32(value), CategorySave.Type.Complet);
				SaveManager.Save(SaveManager.TypeData.Category);
			}
			//PlayerPrefs.SetInt($"Category_{_id}_{_nameMode}_complet", Convert.ToInt32(value));
		}
	}
	public StatusMode.Style.Type Type 
	{ 
		get 
		{
			string type = ""; //PlayerPrefs.GetString($"Category_{_id}_{_nameMode}", StatusMode.Style.Type.Green.ToString());
			if (_typeMode == TypeMode.Default)
			{
				type = SaveManager.Categorys.Get($"{_category}", CategorySave.Type.Status).ToString();
				//return Convert.ToBoolean(PlayerPrefs.GetInt($"Category_{_id}_{_nameMode}_locker", Convert.ToInt32(_isLock)));
			}
			else
			{
				type = SaveManager.Categorys.Get($"{_typeMode}", CategorySave.Type.Status).ToString();
			}
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
			if (IsLock)
			{
				return StatusMode.Style.Type.Red;
			}
			return _type; 
		}
		set
		{
			if (_typeMode == TypeMode.Default)
			{
				SaveManager.Categorys.Set($"{_category}", value.ToString(), CategorySave.Type.Status);
				SaveManager.Save(SaveManager.TypeData.Category);
			}
			else
			{
				SaveManager.Categorys.Set($"{_typeMode}", value.ToString(), CategorySave.Type.Status);
				SaveManager.Save(SaveManager.TypeData.Category);
			}
			//PlayerPrefs.SetString($"Category_{_id}_{_nameMode}", value.ToString());
		}
	}
	public TypeMode TypeMode { get { return _typeMode;} }

	public void Locker(bool isLock)
	{
		IsLock = isLock;
		Type =  isLock ? StatusMode.Style.Type.Red : StatusMode.Style.Type.Green;
	}
}
