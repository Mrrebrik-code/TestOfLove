using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultHolder : MonoBehaviour
{
	[SerializeField] private TMP_Text _resultText;
	[SerializeField] private TMP_Text _resultCountText;
	public void Show(TestHandler.Result result)
	{
		GameManager.CompletCategory();
		gameObject.SetActive(true);
		_resultText.text = Localization.Instance.Localize(result.GetResult());
		_resultCountText.text = $"Баллы:{result.ResultTest}";
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
		YandexSDK.Instance.SetLeaderboardScore("LiderboardLove", PlayerPrefs.GetInt("Result_score"), "Very good!");
		
	}
}
