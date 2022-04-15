using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonUpdate : MonoBehaviour
{
	[SerializeField] private ColorObject[] _objects;
	private void Awake()
	{
		var button = GetComponent<Button>();
		button.onClick.AddListener(() =>
		{

		});
	}
}
