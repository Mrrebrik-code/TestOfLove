using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesManager : SingletonMono<ResourcesManager>
{
	public List<Letter> Letters = new List<Letter>();
	public List<Bonus> Bonuses = new List<Bonus>();
	public List<ScrollObejct> ScrollObjects = new List<ScrollObejct>();
	public List<Product> Products = new List<Product>();
	public Dictionary<Categorys, Category> Categories = new Dictionary<Categorys, Category>();
	private Action onInit;
	public void Initialization(Action callback)
	{
		Letters = LoadLetters();
		Bonuses = LoadBonuses();
		ScrollObjects = LoadScrollObjcts();
		Products = LoadProducts();
		Categories = LoadCategorys();
		SaveManager.Load();
		onInit += callback;
		if (DeviceManager.Instance._isUnity)
		{
			onInit?.Invoke();
		}
		else
		{
			YandexSDK.Instance.onLoadData += (data) =>
			{
				onInit?.Invoke();
			};
		}
		
		
	}
	private List<Letter> LoadLetters()
	{
		var letters = Resources.LoadAll<Letter>("Letters");
		return letters.ToList();
	}

	private List<Bonus> LoadBonuses()
	{
		var bonuses = Tools.BubbleSort(Resources.LoadAll<Bonus>("Bonuses"));
		return bonuses.ToList();
	}

	private List<ScrollObejct> LoadScrollObjcts()
	{
		var objects = Resources.LoadAll<ScrollObejct>("ScrollObjects");
		Tools.BubbleSort(objects);
		var list = objects.ToList();
/*		if (DeviceManager.Instance.Type == Devices.Desktop)
		{
			ScrollObejct delObj = null;
			foreach (var obj in objects)
			{
				if (obj.TypeMode == TypeMode.VIPScaner)
				{
					delObj = obj;
					break;
				}
			}
			list.Remove(delObj);
		}
		else
		{
			List<ScrollObejct> _delObjects = new List<ScrollObejct>();
			foreach (var obj in objects)
			{
				if (obj.TypeMode == TypeMode.VIPNamesToTree || obj.TypeMode == TypeMode.VIPTestNames)
				{
					_delObjects.Add(obj);
				}
			}

			foreach (var item in _delObjects)
			{
				list.Remove(item);
			}
		}*/

		
		return list;
	}

	private List<Product> LoadProducts()
	{
		var products = Resources.LoadAll<Product>("Products");
		return products.ToList();
	}

	private Dictionary<Categorys, Category> LoadCategorys()
	{
		var categorys = Resources.LoadAll<Category>("Categorys");
		var _categories = new Dictionary<Categorys, Category>();

		foreach (var categoryTemp in categorys)
		{
			_categories.Add(categoryTemp.Categorys, categoryTemp);
		}

		return _categories;
	}
}
