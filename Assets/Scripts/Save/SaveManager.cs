using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.Linq;

public static class SaveManager
{
	public static List<ColorObject> _colorsObjects = new List<ColorObject>();

	public static void Subscribe(ColorObject colorObject)
	{
		_colorsObjects.Add(colorObject);
	}

	public static void Save()
	{
		Data[] _datas = new Data[_colorsObjects.Count];

		for (int i = 0; i < _colorsObjects.Count; i++)
		{
			_datas[i] = new Data(_colorsObjects[i].Index, _colorsObjects[i].gameObject.name);
		}

		var data = new SaveData(_datas);

		YandexSDK.Instance.SaveDataTest(data);
	}

	public static void Load()
	{
		YandexSDK.Instance.LoadDataTest((data) =>
		{
			var saveData = JsonUtility.FromJson<SaveData>(data);
			foreach (var datas in saveData.data)
			{
				foreach (var item in _colorsObjects)
				{
					if(item.gameObject.name == datas.name)
					{
						item.SetColor(datas.index);
						break;
					}
				}
			}
		});

	}

	public static void Reboot()
	{
		foreach (var color in _colorsObjects)
		{
			color.SetColor(0);
		}
	}
}
