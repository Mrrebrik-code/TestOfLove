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
		gameObject.SetActive(true);
		_resultText.text = Localization.Instance.Localize(result.GetResult());
		_resultCountText.text = $"Баллы:{result.ResultTest}";
	}
}
