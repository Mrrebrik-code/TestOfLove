using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionHolder : MonoBehaviour
{
	private Category _category;
	[SerializeField] private TMP_Text _questionText;
	[SerializeField] private TMP_Text _counterText;
	public void SetQuestion(Question question)
	{
		_questionText.text = Localization.Instance.Localize(question.QuestionText);
	}
	public void UpdateCounter(int countCurrent, int count)
	{
		_counterText.text = $"{countCurrent}/{count}";
	}
}
