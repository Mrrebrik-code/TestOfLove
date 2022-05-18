using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

public static class SaveManager
{
	public static BankSave Bank = null;
	public static CategorySave Categorys = null;
	public enum TypeData
	{
		Bank,
		Category
	}
	public static List<ColorObject> _colorsObjects = new List<ColorObject>();

	public static void Subscribe(ColorObject colorObject)
	{
		_colorsObjects.Add(colorObject);
	}

	public static void Save(ISaver data, TypeData type)
	{
/*		using (StreamWriter file = File.CreateText($"{UnityEngine.Application.streamingAssetsPath}/{type}.data"))
		{
			JsonSerializer serializer = new JsonSerializer();
			serializer.Serialize(file, data);
		}*/
		YandexSDK.Instance.SaveDataTest(data);

		switch (type)
		{
			case TypeData.Bank: Bank = (BankSave)data; break;
			case TypeData.Category: Categorys = (CategorySave)data; break;
		}
	}

	public static void Save(TypeData type)
	{
		switch (type)
		{
			case TypeData.Bank:
				Save(Bank, type);
				break;
			case TypeData.Category:
				Save(Categorys, type);
				break;
		}
	}

	public static void Load()
	{
		if (YandexSDK.Instance.IsAuth)
		{
			YandexSDK.Instance.LoadDataTest((data) =>
			{
				Bank = JsonConvert.DeserializeObject<BankSave>(data);
				try
				{
					Categorys = JsonConvert.DeserializeObject<CategorySave>(data);
				}
				catch
				{
					Save(new CategorySave(), TypeData.Category);
				}
			});
		}
		else
		{
			Bank = LoadFile<BankSave>($"{UnityEngine.Application.streamingAssetsPath}/{TypeData.Bank}.data");
			if(Bank == null) Save(new BankSave(), TypeData.Bank);

			Categorys = LoadFile<CategorySave>($"{UnityEngine.Application.streamingAssetsPath}/{TypeData.Category}.data");
			if (Categorys == null) Save(new CategorySave(), TypeData.Category);
		}
		
	}

	private static T LoadFile<T>(string path) where T : class
	{
		try
		{
			using (StreamReader dataFile = new StreamReader(path))
			{

				string data = dataFile.ReadToEnd();
				return JsonConvert.DeserializeObject<T>(data);
			}
		}
		catch
		{
			return null;
		}
		
	}
}
