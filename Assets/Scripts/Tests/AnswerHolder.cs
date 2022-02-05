using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerHolder : MonoBehaviour
{
	public Action<Question.Answer> onClick;
	private Question.Answer _answer;
	private Button _button;
	[SerializeField] private TMP_Text _anserText;
	public void SetAnswer(Question.Answer answer)
	{
		_answer = answer;
		_anserText.text = answer.AnswerText;
		if (_button == null) _button = GetComponent<Button>();

		_button.ListenerButton(Click);
	}

	private void Click()
	{
		onClick?.Invoke(_answer);
	}

	public void Delete()
	{
		DestroyImmediate(gameObject);
	}
}
