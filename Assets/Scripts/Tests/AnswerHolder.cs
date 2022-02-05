using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerHolder : MonoBehaviour
{
	private Question.Answer _answer;
	[SerializeField] private TMP_Text _anserText;
	public void SetAnswer(Question.Answer answer)
	{
		_answer = answer;
		_anserText.text = answer.AnswerText;
	}

	public void Delete()
	{
		DestroyImmediate(gameObject);
	}
}
