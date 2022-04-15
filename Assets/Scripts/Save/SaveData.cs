using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
	public Data[] data;

	public SaveData(Data[] data)
	{
		this.data = data;
	}
}

[System.Serializable]
public class Data
{
	public int index;
	public string name;

	public Data(int index, string name)
	{
		this.index = index;
		this.name = name;
	}
}
