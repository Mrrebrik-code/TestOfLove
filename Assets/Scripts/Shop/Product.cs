using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product", menuName = "Custom/Product")]
public class Product : ScriptableObject
{
	[SerializeField] private int _price;
	[SerializeField] private int _count;
	[SerializeField] private int _benefit;
	[SerializeField] private Sprite _icon;
	[SerializeField] private string _idPurchase;

	public int Price { get { return _price; } }
	public int Count { get { return _count; } }
	public int Benefit { get { return _benefit; } }
	public Sprite Icon { get { return _icon; } }
	public string IdPurchase { get { return _idPurchase; } }
}
