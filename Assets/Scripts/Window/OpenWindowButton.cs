using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenWindowButton : MonoBehaviour
{
	[SerializeField] private Window _windowOpen;
	private Button _button;
	private void Awake()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(() =>
		{
			WindowManager.Instance.HandleCurrentActiveWindow(_windowOpen);
		});
	}
}
