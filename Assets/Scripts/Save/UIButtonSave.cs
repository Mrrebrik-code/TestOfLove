using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonSave : MonoBehaviour
{
	private void Awake()
	{
		var button = GetComponent<Button>();
		button.onClick.AddListener(() =>
		{
		
		});
	}
}
