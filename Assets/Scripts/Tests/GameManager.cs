using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager 
{
	public static Category Category { get; private set; }
	private static Dictionary<Categorys, Category> _categories = new Dictionary<Categorys, Category>();

	public static bool SetCategory(Categorys category)
	{
		if (_categories.Count <= 0) LoadCategorys();

		if (_categories.ContainsKey(category) == false) return false; 

		Category = _categories[category];
		
		return Category != null;
	}

	private static void LoadCategorys()
	{
		var categorys = Resources.LoadAll<Category>("Categorys");
		foreach (var categoryTemp in categorys)
		{
			_categories.Add(categoryTemp.Categorys, categoryTemp);
		}
	}
}
