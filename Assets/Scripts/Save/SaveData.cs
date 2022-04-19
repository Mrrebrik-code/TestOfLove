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
	public int hearts;
	public string[] categorysComplet;
	public int currentStreak;
	public string lastTakeTime;
	public string currentLanguage;
	public string[] viewLetters;
	public string[] takeRewardLetters;
	public string[] deleteLetters;
	public string[] creatLetters;
	public string[] dataLetters;
	public string[] takeReadLetter;
	public string[] notificationData;


	public Data(int index, string name)
	{
		this.index = index;
		this.name = name;
	}
}
