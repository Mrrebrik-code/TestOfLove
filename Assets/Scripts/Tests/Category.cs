using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Category", menuName = "Custom/Category")]
public class Category : ScriptableObject
{
	[SerializeField] private Categorys _categorys;
	[SerializeField] private List<Question> _questions;
	[SerializeField] private List<Question> _additionalQuestions;
	private ValuesResult _valueResult;

	public ValuesResult Value
	{ 
		get 
		{ 
			if(_valueResult == null)
			{
				_valueResult = new ValuesResult(_categorys);
			}
			return _valueResult;
		} 
	}
	public Categorys Categorys { get { return _categorys; } }
	public List<Question> Questions { get { return _questions; } }
	public List<Question> AdditionalQuestions { get { return _additionalQuestions; } }


	public class ValuesResult 
	{
		private Categorys _category;
		public bool IsInit
		{
			get
			{
				if (PlayerPrefs.HasKey($"{_category}_init"))
				{
					return Convert.ToBoolean(PlayerPrefs.GetInt($"{_category}_init"));
				}
				else
				{
					return false;
				}
			}
			set
			{
				PlayerPrefs.SetInt($"{_category}_init", Convert.ToInt32(value));
			}
		}
		public ValuesResult(Categorys category)
		{
			_category = category;
		}
		public int Max
		{
			get 
			{
				if (PlayerPrefs.HasKey($"{_category}_max"))
				{
					IsInit = true;
					return PlayerPrefs.GetInt($"{_category}_max");
				}
				else
				{
					return 0; //Просчет количества
				}
			}
			set
			{
				PlayerPrefs.SetInt($"{_category}_max", value);
				IsInit = true;
			}
		}

		public int Min
		{
			get
			{
				if (PlayerPrefs.HasKey($"{_category}_min"))
				{
					IsInit = true;
					return PlayerPrefs.GetInt($"{_category}_min");
				}
				else
				{
					return 0; //Просчет количества
				}
			}
			set
			{
				PlayerPrefs.SetInt($"{_category}_min", value);
				IsInit = true;
			}
		}
	}


}
