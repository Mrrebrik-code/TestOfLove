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
	[SerializeField] private NotificationButton _notificationButton;
	private bool _isDayOneOpen = false;

	private int _currentStreak
	{
		get => PlayerPrefs.GetInt("currentStreak", 1);
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
	private float _takeColldown = 24f/* / 24 / 60 / 6 / 2*/;
	private float _takeDeadline = 48f/* / 24 / 60 / 6 / 2*/;

	public override void Awake()
	{
		base.Awake();
		

	}
	private void Start()
	{
		Init();
		SetCurrentDayBonus(_currentStreak);
		StartCoroutine(UpdateStateRewards());
		if (_isTakeReward == true)
		{
			WindowManager.Instance.HandleCurrentActiveWindow(Window.DailyBonus);
		}
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
				_currentStreak = 1;
				SetCurrentDayBonus(_currentStreak);

				MailManager.Instance.AddLetter(Letters.SkipDailyBinus, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "_Menu");

				
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
	private bool _isNotification = false;
	private void UpdateRewardUI()
	{
		if (_isTakeReward)
		{
			Debug.Log(11111);
			_timerDayToButton.SetButtonStatus("core_055", true);
			if(_isNotification == false)
			{
				_isNotification = true;
				_notificationButton.CountNotification = 1;
			}
		}
		else
		{
			
			var nextTimeTake = _dataTime.Value.AddHours(_takeColldown);
			var currentTakeCooldown = nextTimeTake - DateTime.UtcNow;

			string time = $"{currentTakeCooldown.Hours:D2}:{currentTakeCooldown.Minutes:D2}:{currentTakeCooldown.Seconds:D2}";

			_timerDayToButton.SetButtonStatus($"{time} - {Localization.Instance.Localize("core_054")}", false);

		}
	}

	private void Init()
	{
		_bonuses = ResourcesManager.Instance.Bonuses;
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
		if (_isTakeReward != false)
		{
			_timerDayToButton.SetButtonStatus($"Забрать награду", false);
			_isNotification = false;
			_notificationButton.CountNotification = -1;
			_currentBonusDay.Take();
			_dataTime = DateTime.UtcNow;
			if (_currentStreak == _maxStreakCount)
			{
				MailManager.Instance.AddLetter(Letters.FirstDay5Complet, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "_Menu");
				_currentStreak = 1;
				ResetBonuses();
				SetCurrentDayBonus(_currentStreak);
			}
			else
			{
				_currentStreak++;
			}
			SetCurrentDayBonus(_currentStreak);
		}

	}
	
}
