using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Custom/Question")]
public class Question : ScriptableObject
{
	[SerializeField] private string _questionText;
	[SerializeField] private List<Answer> _answers;

	public string QuestionText { get { return _questionText; } }
	public List<Answer> Answers { get { return _answers; } }

	[Serializable]
	public class Answer
	{
		public string AnswerText;
	}
}
