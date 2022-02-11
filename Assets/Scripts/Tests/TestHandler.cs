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
	private int _questionCount;

	[SerializeField] private AnswerHolder _answerHolderPrefab;
	[SerializeField] private Transform _contentToAnswers;

	[SerializeField] private QuestionHolder _question;
	private List<AnswerHolder> _answers = new List<AnswerHolder>();

	[SerializeField] private Result _result;

	[SerializeField] private GameObject _additionalPanel;

	[SerializeField] private List<int> _indexAdditionQuestion = new List<int>();

	private void Start()
	{
		_categoryCurrent = GameManager.Category;
		_questionCount = _categoryCurrent.Questions.Count;
		_result.Init(_categoryCurrent);
		foreach (var question in _categoryCurrent.Questions)
		{
			_questions.Add(question);
		}
		_questions.RandomSorting();

		GenerationIndexAdditionQuestion(_questions.Count);

		foreach (var question in _categoryCurrent.AdditionalQuestions)
		{
			_additionalQuestions.Add(question);
		}
		_additionalQuestions.RandomSorting();

		Init();
	}

	private void GenerationIndexAdditionQuestion(int count)
	{
		var countQuestion = Convert.ToInt32(Math.Round(Convert.ToSingle(count / 4)));

		for (int i = 0; i < countQuestion; i++)
		{
			var index = UnityEngine.Random.Range(3, _questions.Count - 1);

			while (_indexAdditionQuestion.Contains(index))
			{
				index = UnityEngine.Random.Range(3, _questions.Count - 1);
			}

			_indexAdditionQuestion.Add(index);
		}
		_indexAdditionQuestion.Sort();

		for (int i = 0; i < _indexAdditionQuestion.Count; i++)
		{
			if (i + 1 == _indexAdditionQuestion.Count) return;

			if (_indexAdditionQuestion[i + 1] - _indexAdditionQuestion[i] <= 2)
			{
				_indexAdditionQuestion.Remove(i);
			}
		}
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
		_question.UpdateCounter(_numberQuestion, _questionCount);

		if(_indexAdditionQuestion.Contains(_numberQuestion))
		{
			_indexAdditionQuestion.Remove(_numberQuestion);
			_additionalPanel.SetActive(true);
		}

		GenerationAnswer(_currentQuestion.Answers.Count);
		
		_question.SetQuestion(_currentQuestion);

		_questions.Remove(_currentQuestion);
	}

	public void SetAdditionalQuestion()
	{

		if (_additionalQuestions.Count <= 0) return;

		_questionCount++;
		_questions.Add(Instance._currentQuestion);
		var question = _additionalQuestions[_additionalQuestions.Count - 1];

		Init(question, true);
		_additionalPanel.SetActive(false);

		_additionalQuestions.Remove(question);
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
			answerHolder.SetAnswer(_currentQuestion.Answers[i], i + 1);
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
			var result = "Результат";
			var max = _category.Value.Max;
			var min = _category.Value.Min;
			var sred = max / 2;
			var minGood = 0;
			var maxGood = 0;
			var delta = 0;
			var deltaMin = sred - min;

			if (deltaMin > 10) delta = 10;
			else if (deltaMin > 20) delta = 15;
			else delta = 3;

			minGood = sred - delta;
			maxGood = sred + delta;

			if (ResultTest > sred && ResultTest < maxGood || ResultTest < sred && ResultTest > minGood) result = $"{_category.Categorys}_result_good";
			else if (ResultTest > maxGood) result = $"{_category.Categorys}_result_great";
			else result = $"{_category.Categorys}_result_bad";

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
				Instance._question.UpdateCounter(Instance._numberQuestion, Instance._questionCount);
				Instance.Init(question);
			}
		}
	}
}
