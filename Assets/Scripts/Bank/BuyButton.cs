using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
	private Button _button;

	private void Start()
	{
		if(_button == null) _button = GetComponent<Button>();

		_button.onClick.AddListener(() =>
		{
			StackProductBuy.Instance.Buy();
		});
	}
}
