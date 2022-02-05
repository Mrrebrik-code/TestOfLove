using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
	private List<Product> _products = new List<Product>();
	[SerializeField] private ProductHolder _productHolderPrefab;
	[SerializeField] private Transform _content;
	private List<ProductHolder> _productsHolder = new List<ProductHolder>();

	private void Awake()
	{
		Initialization();
	}
	private void Initialization()
	{
		_products = LoadProducts();

		_products.ForEach(product =>
		{
			var productHolder = Instantiate(_productHolderPrefab, _content);
			productHolder.Init(product);
			productHolder.onBuyProduct += Buy;
			_productsHolder.Add(productHolder);
		});
	}
	private List<Product> LoadProducts()
	{
		var products = Resources.LoadAll<Product>("Products");
		return products.ToList();
	}
	public void Buy(Product product)
	{
		Debug.Log("Buy product: " + product.Price + " YAN");
	}
}
