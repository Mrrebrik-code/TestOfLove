using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DailyBonusManager : SingletonMono<DailyBonusManager>
{
	private List<Bonus> _bonuses = new List<Bonus>();
	private List<BonusHolder> _bonusHolders = new List<BonusHolder>();
	[SerializeField] private BonusHolder _bonusHolderPrefab;
	[SerializeField] private Transform _content;
	private BonusHolder _currentBonusDay;
	[SerializeField] private TimerDayToButton _timerDayToButton;

	private int _currentStreak
	{
		get => PlayerPrefs.GetInt("currentStreak", 0);
		set => PlayerPrefs.SetInt("currentStreak", value);
	}
	private DateTime? _dataTime
	{
		get
		{
			string data = PlayerPrefs.GetString("lastTakeTime", null);
			if (!string.IsNullOrEmpty(data)) return DateTime.Parse(data);

			return null;
		}
		set
		{
			if(value != null) PlayerPrefs.SetString("lastTakeTime", value.ToString());
			else PlayerPrefs.DeleteKey("lastTakeTime");

		}
	}

	private bool _isTakeReward;
	private int _maxStreakCount = 5;
	private float _takeColldown = 24f / 24 / 60;
	private float _takeDeadline = 48f / 24 / 60;

	public override void Awake()
	{
		base.Awake();
		Init();
		SetCurrentDayBonus(_currentStreak);
		StartCoroutine(UpdateStateRewards());
	}

	private IEnumerator UpdateStateRewards()
	{
		while (true)
		{
			UpdateRewardState();
			yield return new WaitForSeconds(1f);
		}
	}

	private void UpdateRewardState()
	{
		_isTakeReward = true;

		if (_dataTime.HasValue)
		{
			var time = DateTime.UtcNow - _dataTime.Value;

			if(time.TotalHours > _takeDeadline)
			{
				ResetBonuses();
				
				_dataTime = null;
				_currentStreak = 0;
				SetCurrentDayBonus(_currentStreak);
			}
			else if(time.TotalHours < _takeColldown)
			{
				
				_isTakeReward = false;
			}

			UpdateRewardUI();
		}
	}
	private void ResetBonuses()
	{
		foreach (var bonus in _bonusHolders)
		{
			bonus.ResetBonus();
		}
	}
	private void UpdateRewardUI()
	{
		if (_isTakeReward)
		{
			_timerDayToButton.SetButtonStatus("Забрать награду", true);
		}
		else
		{
			var nextTimeTake = _dataTime.Value.AddHours(_takeColldown);
			var currentTakeCooldown = nextTimeTake - DateTime.UtcNow;

			string time = $"{currentTakeCooldown.Hours:D2}:{currentTakeCooldown.Minutes:D2}:{currentTakeCooldown.Seconds:D2}";

			_timerDayToButton.SetButtonStatus($"{time} - до получения награды!", false);

		}
	}

	private void Init()
	{
		_bonuses = LoadBonuses();
		if (_bonuses == null) return;

		_bonuses.ForEach(bonus =>
		{
			var bonusHolder = Instantiate(_bonusHolderPrefab, _content);
			bonusHolder.Init(bonus);
			_bonusHolders.Add(bonusHolder);
		});
	}

	private void SetCurrentDayBonus(int streak)
	{
		BonusHolder currentBonus = null;
		foreach (var bonusHolder in _bonusHolders)
		{
			if(bonusHolder.Bonus.IsTake == false && bonusHolder.Bonus.Id == streak)
			{
				currentBonus = bonusHolder;
				break;
			}
		}
		if(currentBonus != null)
		{
			_currentBonusDay = currentBonus;
			_currentBonusDay.SetStyle(BonusHolder.Style.Type.Current);
		}
	}
	
	public void Take()
	{


		_dataTime = DateTime.UtcNow;
		_currentStreak = (_currentStreak + 1) % _maxStreakCount;
		if (_isTakeReward != false)
		{
			_currentBonusDay.Take();
			SetCurrentDayBonus(_currentStreak);
		}

	}
	private List<Bonus> LoadBonuses()
	{
		var bonuses = Tools.BubbleSort(Resources.LoadAll<Bonus>("Bonuses"));
		return bonuses.ToList();
	}
}
