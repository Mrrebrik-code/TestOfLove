using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsCategory : MonoBehaviour
{
	[SerializeField] private SettingsHandler.Category _category;
	public SettingsHandler.Category Category { get { return _category; } }
	public void Open()
	{
		gameObject.SetActive(true);
	}
	public void Close()
	{
		gameObject.SetActive(false);
	}
}
