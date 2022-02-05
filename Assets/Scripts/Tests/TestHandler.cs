using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHandler : SingletonMono<TestHandler>
{
	private History _history = new History();
	private Category _categoryCurrent;
	private List<Question> _questions = new List<Question>();
	private Question _currentQuestion;
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

	private void Init(Question question = null)
	{
		if(question != null)
		{
			_answers.ForEach(answerTemp =>
			{
				answerTemp.onClick -= HandleClickAnswer;
				answerTemp.Delete();
			});
			_answers.Clear();

			_currentQuestion = question;
			_question.SetQuestion(_currentQuestion);
			GenerationAnswer(_currentQuestion.Answers.Count);
			return;
		}
		_currentQuestion = _questions[_questions.Count - 1];
		_numberQuestion++;

		GenerationAnswer(_currentQuestion.Answers.Count);
		
		_question.SetQuestion(_currentQuestion);

		_questions.Remove(_currentQuestion);
	}
	private void GenerationAnswer(int count)
	{
		for (int i = 0; i < count; i++)
		{
			AnswerHolder answerHolder = Instantiate(_answerHolderPrefab, _contentToAnswers);
			answerHolder.SetAnswer(_currentQuestion.Answers[i]);
			answerHolder.onClick += HandleClickAnswer;
			_answers.Add(answerHolder);
		}
	}

	private void HandleClickAnswer(Question.Answer answer)
	{
		_history.Enqueue(answer);
		_answers.ForEach(answerTemp =>
		{
			answerTemp.onClick -= HandleClickAnswer;
			answerTemp.Delete();
		});
		_answers.Clear();
		Init();
	}

	public void Undo()
	{
		_history.Undo();
	}

	private class History
	{
		private Dictionary<Question, Question.Answer> _questionToSelectedAnswer = new Dictionary<Question,Question.Answer>();
		private List<Question> _questions = new List<Question>();
		public void Enqueue(Question.Answer answer)
		{
			_questions.Add(Instance._currentQuestion);
			_questionToSelectedAnswer.Add(Instance._currentQuestion, answer);
		}

		public Question Peek()
		{
			return _questions[_questions.Count - 1];
		}

		public Question Dequeue()
		{
			var temp = _questions[_questions.Count - 1];
			_questions.Remove(temp);
			_questionToSelectedAnswer.Remove(temp);

			return temp;
		}

		public void Undo()
		{
			if (_questions.Count > 0)
			{
				Instance._questions.Add(Instance._currentQuestion);
				var question = Dequeue();
				Instance.Init(question);
				//Логика отмены
			}
		}
	}
}
