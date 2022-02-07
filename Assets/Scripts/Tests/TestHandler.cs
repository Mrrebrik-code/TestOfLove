using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHandler : SingletonMono<TestHandler>
{
	private History _history = new History();
	private Category _categoryCurrent;
	private List<Question> _questions = new List<Question>();
	private List<Question> _additionalQuestions = new List<Question>();
	private Question _currentQuestion;
	private int _numberQuestion;

	[SerializeField] private AnswerHolder _answerHolderPrefab;
	[SerializeField] private Transform _contentToAnswers;

	[SerializeField] private QuestionHolder _question;
	private List<AnswerHolder> _answers = new List<AnswerHolder>();

	[SerializeField] private Result _result;

	[SerializeField] private GameObject _additionalPanel;

	private void Start()
	{
		_categoryCurrent = GameManager.Category;
		_result.Init(_categoryCurrent);
		foreach (var question in _categoryCurrent.Questions)
		{
			_questions.Add(question);
		}
		_questions.RandomSorting();

		foreach (var question in _categoryCurrent.AdditionalQuestions)
		{
			_additionalQuestions.Add(question);
		}
		_additionalQuestions.RandomSorting();

		Init();
	}

	private void Init(Question question = null, bool isAdditional = false)
	{
		if(question != null)
		{
			if(isAdditional == false)
			{
				ClearAnswers();

				_currentQuestion = question;
				_question.SetQuestion(_currentQuestion);

				GenerationAnswer(_currentQuestion.Answers.Count);
				return;
			}
			else
			{
				ClearAnswers();
				_currentQuestion = question;
				_question.SetQuestion(_currentQuestion);
				GenerationAnswer(_currentQuestion.Answers.Count);
				return;
			}
		}

		if(_questions.Count <= 0)
		{
			_result.Show();
			return;
		}

		_currentQuestion = _questions[_questions.Count - 1];

		_numberQuestion++;
		_question.UpdateCounter(_numberQuestion);

		/*if (_numberQuestion == 3) _additionalPanel.SetActive(true);*/ //Прописать систему показа доп вопроса!

		GenerationAnswer(_currentQuestion.Answers.Count);
		
		_question.SetQuestion(_currentQuestion);

		_questions.Remove(_currentQuestion);
	}

	public void SetAdditionalQuestion()
	{

		if (_additionalQuestions.Count <= 0) return;

		_questions.Add(Instance._currentQuestion);
		var question = _additionalQuestions[_additionalQuestions.Count - 1];

		Init(question, true);
		_additionalPanel.SetActive(false);
	}
	private void ClearAnswers()
	{
		_answers.ForEach(answerTemp =>
		{
			answerTemp.onClick -= HandleClickAnswer;
			answerTemp.Delete();
		});
		_answers.Clear();
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
		_result.Add(answer.Massa);

		ClearAnswers();
		Init();
	}

	public void Undo()
	{
		_history.Undo();
	}

	[Serializable]
	public class Result
	{
		public int ResultTest { get; private set; }
		private Category _category;
		[SerializeField] private ResultHolder _resultHolder;

		public void Init(Category category)
		{
			_category = category;
		}
		public void Show()
		{
			_resultHolder.Show(this);
		}
		public string GetResult()
		{
			var result = "Результат"; //Прописать систему выборки результата за счет количества массы ответов

			return result;
		}
		public void Add(int count)
		{
			ResultTest += count;
		}
		public void Subtract(int count)
		{
			ResultTest -= count;
		}
	}

	[Serializable]
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

		public Question Dequeue(bool isDictionary = true)
		{
			var temp = _questions[_questions.Count - 1];
			_questions.Remove(temp);

			if(isDictionary) _questionToSelectedAnswer.Remove(temp);


			return temp;
		}

		public void Undo()
		{
			if (_questions.Count > 0)
			{
				Instance._questions.Add(Instance._currentQuestion);

				var question = Dequeue(isDictionary: false);

				Instance._result.Subtract(_questionToSelectedAnswer[question].Massa);

				_questionToSelectedAnswer.Remove(question);

				Instance._numberQuestion--;
				Instance._question.UpdateCounter(Instance._numberQuestion);
				Instance.Init(question);
			}
		}
	}
}
