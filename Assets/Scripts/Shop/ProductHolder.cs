using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ProductHolder : MonoBehaviour
{
	public Action<Product> onBuyProduct;

	private Product _product;
	[SerializeField] private TMP_Text _priceText;
	[SerializeField] private TMP_Text _countText;
	[SerializeField] private TMP_Text _benefitText;
	[SerializeField] private Image _icon;
	[SerializeField] private Button _button;
	[SerializeField] private GameObject _benefitObject;

	public void Init(Product product)
	{
		_product = product;
		_priceText.text = $"{product.Price} YAN";
		_countText.text = $"{product.Count} шт.";

		var isBenefit = product.Benefit != 0;
		if (isBenefit) _benefitText.text = $"{product.Benefit}% выгода";
		_benefitObject.gameObject.SetActive(isBenefit);

		_icon.sprite = product.Icon;
		_button.ListenerButton(Buy);
	}

	private void Buy()
	{
		onBuyProduct?.Invoke(_product);
	}
}
