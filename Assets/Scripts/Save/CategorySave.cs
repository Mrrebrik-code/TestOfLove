using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorySave : ISaver
{
	public enum Type 
	{ 
		Complet,
		Lock,
		Status
	}

	public Dictionary<string, int> categorysLock = new Dictionary<string, int>()
	{
		{ "Friends",	0 },
		{ "Love",		0 },
		{ "Mutually",	0 },
		{ "Values",		0 },
		{ "Confidence", 0 },
		{ "VIPTestNames", 1 },
		{ "VIPNamesToTree", 1 },
		{ "VIPScaner", 1 }
	};
	public Dictionary<string, int> categorysComplet = new Dictionary<string, int>()
	{
		{ "Friends",	0 },
		{ "Love",		0 },
		{ "Mutually",	0 },
		{ "Values",		0 },
		{ "Confidence", 0 },
		{ "VIPTestNames", 0 },
		{ "VIPNamesToTree", 0 },
		{ "VIPScaner", 0 }
	};
	public Dictionary<string, string> categorysStatus = new Dictionary<string, string>()
	{
		{ "Friends",    "Green" },
		{ "Love",       "Green" },
		{ "Mutually",   "Green" },
		{ "Values",     "Green" },
		{ "Confidence", "Green" },
		{ "VIPTestNames", "Red" },
		{ "VIPNamesToTree", "Red" },
		{ "VIPScaner", "Red" }
	};



	public CategorySave()
	{
	}
	public CategorySave(Dictionary<string, int> categorysComplet)
	{
		this.categorysComplet = categorysComplet;
	}

	public void Set(string category, object data, Type type)
	{
		switch (type)
		{
			case Type.Complet:
				if (categorysComplet.ContainsKey(category)) categorysComplet[category] = (int)data;
				else categorysComplet.Add(category, (int)data);
				break;

			case Type.Lock:
				if (categorysLock.ContainsKey(category)) categorysLock[category] = (int)data;
				else categorysLock.Add(category, (int)data);
				break;

			case Type.Status:
				if (categorysStatus.ContainsKey(category)) categorysStatus[category] = data.ToString();
				else categorysStatus.Add(category, data.ToString());
				break;
		}
		
	}

	public object Get(string data, Type type)
	{
		switch (type)
		{
			case Type.Complet:
				if (categorysComplet.ContainsKey(data)) return categorysComplet[data];
				else return 0;

			case Type.Lock:
				if (categorysLock.ContainsKey(data)) return categorysLock[data];
				else return 0;

			case Type.Status:
				if (categorysStatus.ContainsKey(data)) return categorysStatus[data];
				else return 0;

			default: return null;
		}
		
	}
}
