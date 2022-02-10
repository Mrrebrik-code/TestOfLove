using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager 
{
	private static ScrollObejct _currentScrollObject;
	public static Category Category { get; private set; }
	private static Dictionary<Categorys, Category> _categories = new Dictionary<Categorys, Category>();

	public static bool SetCategory(Categorys category, ScrollObejct scrollObject)
	{
		_currentScrollObject = scrollObject;

		if (_categories.Count <= 0) _categories = ResourcesManager.Instance.Categories;

		if (_categories.ContainsKey(category) == false) return false; 

		Category = _categories[category];
		if (Category.Value.IsInit == false)
		{
			ResultCalculationMaxAndMin();
		}
		
		return Category != null;
	}

	public static void CompletCategory()
	{
		_currentScrollObject.Type = StatusMode.Style.Type.Yellow;
		switch (Category.Categorys)
		{
			case Categorys.Love:
				MailManager.Instance.AddLetter(Letters.CompletLoveTest, false);
				break;
			case Categorys.Confidence:
				MailManager.Instance.AddLetter(Letters.CompletConfidenceTest, false);
				break;
			case Categorys.Mutually:
				MailManager.Instance.AddLetter(Letters.CompletMutuallyTest, false);
				break;
			case Categorys.Values:
				MailManager.Instance.AddLetter(Letters.CompletValuesTest, false);
				break;
			case Categorys.Friends:
				MailManager.Instance.AddLetter(Letters.CompletFriendshipTest, false);
				break;
		}
		PlayerPrefs.SetInt($"{Category.Categorys}_complet", 1);

		if(CheckCompletTestAll()) MailManager.Instance.AddLetter(Letters.CompletAllTests, false);
	}

	private static bool CheckCompletTestAll()
	{
		var categorys = new Categorys[5] { Categorys.Friends, Categorys.Love, Categorys.Mutually, Categorys.Values, Categorys.Confidence };
		var indexes = new List<int>();
		foreach (var category in categorys)
		{
			indexes.Add(PlayerPrefs.GetInt($"{category}_complet"));
		}
		var complets = 0;
		foreach (var index in indexes)
		{
			complets += index;
		}

		if(complets == 3) MailManager.Instance.AddLetter(Letters.HelperVIPModesMore, false);

		return complets == 5;
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
