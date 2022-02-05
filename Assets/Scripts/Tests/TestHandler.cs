using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHandler : MonoBehaviour
{
	private Category _categoryCurrent;
	private List<Question> _questions = new List<Question>();
	private int _numberQuestion;

	[SerializeField] private AnswerHolder _answerHolderPrefab;
	[SerializeField] private Transform _contentToAnswers;

	[SerializeField] private QuestionHolder _question;
	private List<AnswerHolder> _answers = new List<AnswerHolder>();

	private void Start()
	{
		_categoryCurrent = GameManager.Category;
		foreach (var question in _categoryCurrent.Questions)
		{
			_questions.Add(question);
		}
		_questions.RandomSorting();

		Init();
	}

	private void Init()
	{
		Question question = _questions[_answers.Count];
		int countAnswers = question.Answers.Count;

		for (int i = 0; i < countAnswers; i++)
		{
			AnswerHolder answerHolder = Instantiate(_answerHolderPrefab, _contentToAnswers);
			answerHolder.SetAnswer(question.Answers[i]);

			_answers.Add(answerHolder);
		}
		_question.SetQuestion(question);

		_questions.Remove(question);
	}
}
