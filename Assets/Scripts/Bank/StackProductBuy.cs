using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StackProductBuy : SingletonMono<StackProductBuy>
{
	private ProductStack _currentPtoductStack;
	private Action _buyAction;
	[SerializeField] private TMP_Text _priceText;
	public void Init(int price, Action callback)
	{
		_currentPtoductStack = new ProductStack(price, callback);
		_priceText.text = price.ToString();
	}

	public void Buy()
	{
		Bank.BankManager.Instance.Heart.Withdraw(_currentPtoductStack.Price);
		_currentPtoductStack.BuyAction();
	}

	public class ProductStack
	{
		public int Price { get; private set; }
		public Action BuyAction { get; private set; } = null;
		public ProductStack(int price, Action callback)
		{
			Price = price;
			BuyAction = callback;
		}
	}
}
