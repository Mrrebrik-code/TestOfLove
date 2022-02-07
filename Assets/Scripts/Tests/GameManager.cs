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
		if (Category.Value.IsInit == false)
		{
			ResultCalculationMaxAndMin();
		}
		
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

	private static void ResultCalculationMaxAndMin()
	{
		int max = 0;
		int min = 0;
		foreach (var question in Category.Questions)
		{
			var listMassa = new int[question.Answers.Count];

			for (int i = 0; i < listMassa.Length; i++)
			{
				listMassa[i] = question.Answers[i].Massa;
			}

			var massa = Tools.BubbleSort(listMassa);

			min += massa[0];
			max += massa[massa.Length - 1];
		}

		Category.Value.Min = min;
		Category.Value.Max = max;
		Debug.Log("Min: " + Category.Value.Min);
		Debug.Log("Max: " + Category.Value.Max);
	}
}
