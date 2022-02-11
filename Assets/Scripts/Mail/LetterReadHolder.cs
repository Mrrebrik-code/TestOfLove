using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LetterReadHolder : MonoBehaviour
{
	private Letter _letter;
	[SerializeField] private TMP_Text _tiitleText;
	[SerializeField] private TMP_Text _temaText;
	[SerializeField] private TMP_Text _dataText;

	[SerializeField] private GameObject _rewardTextGameObject;
	[SerializeField] private TMP_Text _defaultText;

	[SerializeField] private TMP_Text _promoCodeText;
	[SerializeField] private TMP_Text _heartText;

	[SerializeField] private Button _button;


	public void Init(Letter letter)
	{
		_letter = letter;
		_tiitleText.text = Localization.Instance.Localize(letter.Tittle);
		_temaText.text = Localization.Instance.Localize(letter.Tema);
		_dataText.text = letter.Data.ToShortDateString();

		switch (letter.TypeLetter)
		{
			case TypeLetter.Default:
				_rewardTextGameObject.SetActive(false);
				_defaultText.gameObject.SetActive(true);
				_defaultText.text = Localization.Instance.Localize(letter.FullDescription); 
				break;
			case TypeLetter.RewardPromoCode:
				_rewardTextGameObject.SetActive(true);
				_rewardTextGameObject.GetComponent<TMP_Text>().text = Localization.Instance.Localize(letter.FullDescription);
				_promoCodeText.gameObject.SetActive(true);
				_heartText.gameObject.SetActive(false);
				_promoCodeText.text = letter.PromoCode;
				_button.interactable = false;
				_defaultText.gameObject.SetActive(false);
				break;
			case TypeLetter.RewardHeart:
				_rewardTextGameObject.SetActive(true);
				_rewardTextGameObject.GetComponent<TMP_Text>().text = Localization.Instance.Localize(letter.FullDescription);
				_heartText.gameObject.SetActive(true);
				_promoCodeText.gameObject.SetActive(false);

				_heartText.text = LocalizationText(letter);
				Localization.Instance.Localize(LocalizationText(letter));
				if(PlayerPrefs.HasKey($"{_letter.Tittle}_{_letter.Tema}_take") == false)
				{
					_button.interactable = true;
				}
				else
				{
					_button.interactable = false;

				}
				

				_defaultText.gameObject.SetActive(false);
				break;
		}
	}
	private string LocalizationText(Letter letter)
	{
		if(PlayerPrefs.HasKey($"{_letter.Tittle}_{_letter.Tema}_take") == false)
		{
			return $"{Localization.Instance.Localize("core_060")} " + letter.CountRewardHeart.ToString();
		}
		else
		{
			return $"{Localization.Instance.Localize("core_061")}";
		}
		
	}
	public void RewardHeart()
	{
		if(PlayerPrefs.HasKey($"{_letter.Tittle}_{_letter.Tema}_take") == false)
		{
			_letter.IsTakeReward = true;
			Bank.BankManager.Instance.Heart.Put(_letter.CountRewardHeart);
			PlayerPrefs.SetInt($"{_letter.Tittle}_{_letter.Tema}_take", 1);
			_button.interactable = false;
			_heartText.text = LocalizationText(_letter);
		}
		
	}
}
