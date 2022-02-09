using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MailManager : SingletonMono<MailManager>
{
	public NotificationButton _notificationButton;
	private List<Letter> _letterList = new List<Letter>();
	[SerializeField] private LetterHolder _letterHolderPrefab;
	[SerializeField] private Transform _content;
	[SerializeField] private List<LetterHolder> _letterHolders = new List<LetterHolder>();

	public override void Awake()
	{
		base.Awake();
		Init();
	}
	private void Init()
	{
		_letterList = LoadLetters();
		if (_letterList == null) return;

		_letterList.ForEach(letter =>
		{
			if (letter.IsCreating)
			{
				CreateMessage(letter);
			}
		});
	}
	private List<Letter> LoadLetters()
	{
		var letters = Resources.LoadAll<Letter>("Letters");
		return letters.ToList();
	}

	public void AddLetter(Letters letters)
	{
		foreach (var letter in _letterList)
		{
			if(letter.Type == letters)
			{
				letter.IsCreating = true;
				CreateMessage(letter);
				return;
			}
		}
	}

	public void CreateMessage(Letter letter)
	{
		if (!letter.IsView)
		{
			_notificationButton.CountNotification = 1;
		}

		var letterTemp = Instantiate(_letterHolderPrefab, _content);
		letterTemp.Init(letter);
		_letterHolders.Add(letterTemp);
	}
}
