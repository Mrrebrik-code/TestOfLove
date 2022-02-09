using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScaner : MonoBehaviour
{
	[SerializeField] private TMP_Text _resultText;
	[SerializeField] private List<string> _results = new List<string>();
	public void Init() 
	{
		gameObject.SetActive(true);

		var result = _results[Random.Range(0, _results.Count - 1)];
		_resultText.text = Localization.Instance.Localize(result);
		MailManager.Instance.AddLetter(Letters.CompletScanerMode, false);
	}
}
