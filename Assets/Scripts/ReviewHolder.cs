using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReviewHolder : MonoBehaviour
{
	[SerializeField] private TMP_Text _reviewText;
	[SerializeField] private Button _reviewButton;


	private void Start()
	{
		_reviewText.text = GetString();
		Localization.Instance.Subscribe(() =>
		{
			_reviewText.text = GetString();
		});

		if (GameManager.IsReview == false)
		{
			_reviewButton.interactable = true;
			_reviewButton.onClick.AddListener(Review);
		}
		else
		{
			_reviewButton.interactable = false;
		}
	}

	private string GetString()
	{
		if (GameManager.IsReview == false)
		{
			return $"{Localization.Instance.Localize("core_056")}\n{Localization.Instance.Localize("core_057")}";
		}
		else
		{
			return $"{Localization.Instance.Localize("core_058")}\n{Localization.Instance.Localize("core_059")}";
		}
		
	}

	public void Review()
	{
		if (GameManager.IsReview == false)
		{
			//YandexSDK.Instance.Review();
			GameManager.IsReview = true;
			_reviewButton.interactable = false;
			_reviewText.text = GetString();
		}
	}
}
