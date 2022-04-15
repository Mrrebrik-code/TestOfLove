using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultHolder : MonoBehaviour
{
	[SerializeField] private TMP_Text _resultText;
	[SerializeField] private TMP_Text _resultCountText;
	[SerializeField] private TMP_Text _tittleText;
	public void Show(TestHandler.Result result)
	{
		var categoryText = "";
		switch (result.Category.Categorys)
		{
			case Categorys.Love:
				categoryText = Localization.Instance.Localize("core_017");
				break;
			case Categorys.Confidence:
				categoryText = Localization.Instance.Localize("core_015");
				break;
			case Categorys.Mutually:
				categoryText = Localization.Instance.Localize("core_014");
				break;
			case Categorys.Values:
				categoryText = Localization.Instance.Localize("core_019");
				break;
			case Categorys.Friends:
				categoryText = Localization.Instance.Localize("core_016");
				break;
		}
		_tittleText.text = $"{Localization.Instance.Localize("core_078")} \"{categoryText}\"!\n{Localization.Instance.Localize("core_077")}";

		GameManager.CompletCategory();
		gameObject.SetActive(true);
		_resultText.text = Localization.Instance.Localize(result.GetResult());
		_resultCountText.text = $"{Localization.Instance.Localize("core_084")}:{result.ResultTest}";
		if (PlayerPrefs.HasKey("Result_score"))
		{
			var count = PlayerPrefs.GetInt("Result_score");
			count += result.ResultTest;
			PlayerPrefs.SetInt("Result_score", count);
		}
		else
		{
			PlayerPrefs.SetInt("Result_score", result.ResultTest);
		}
		//YandexSDK.Instance.SetLeaderboardScore("LiderboardLove", PlayerPrefs.GetInt("Result_score"), "Very good!");
		
	}
}
