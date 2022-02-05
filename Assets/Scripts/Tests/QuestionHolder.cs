using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionHolder : MonoBehaviour
{
	[SerializeField] private TMP_Text _questionText;
	public void SetQuestion(Question question)
	{
		_questionText.text = question.QuestionText;
	}
}
