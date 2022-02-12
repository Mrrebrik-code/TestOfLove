using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
	private List<Product> _products = new List<Product>();
	[SerializeField] private ProductHolder _productHolderPrefab;
	[SerializeField] private Transform _content;
	private List<ProductHolder> _productsHolder = new List<ProductHolder>();
	[SerializeField] private TMP_Text _tittleText;
	private void Awake()
	{
		Initialization();

		_tittleText.text = $"{Localization.Instance.Localize("core_040")}\n{Localization.Instance.Localize("core_041")}";
		Localization.Instance.Subscribe(() =>
		{
			_tittleText.text = $"{Localization.Instance.Localize("core_040")}\n{Localization.Instance.Localize("core_041")}";
		});
	}
	private void Initialization()
	{
		YandexSDK.Instance.onPurchaseComplet += PurchaseProductSuccessful;
		YandexSDK.Instance.onPurchaseError += PurchaseProductError;
		_products = ResourcesManager.Instance.Products;
		if (_products == null) return;

		_products.ForEach(product =>
		{
			var productHolder = Instantiate(_productHolderPrefab, _content);
			productHolder.Init(product);
			productHolder.onBuyProduct += Buy;
			_productsHolder.Add(productHolder);
		});
	}

	public void Buy(Product product)
	{
		Debug.Log(product.IdPurchase);
		if (YandexSDK.Instance.IsPurchase)
		{

			YandexSDK.Instance.BuyPurchase(product.IdPurchase);
		}
		Debug.Log("Buy product: " + product.Price + " YAN");
	}

	private void PurchaseProductSuccessful(string id)
	{
		foreach (var product in _products)
		{
			if(product.IdPurchase == id)
			{
				Bank.BankManager.Instance.Heart.Put(product.Count);
				return;
			}
		}
	}

	private void PurchaseProductError()
	{
		Debug.Log("Buy FAILED:");
	}

	private void OnDestroy()
	{
		YandexSDK.Instance.onPurchaseComplet -= PurchaseProductSuccessful;
		YandexSDK.Instance.onPurchaseError -= PurchaseProductError;
	}
}
