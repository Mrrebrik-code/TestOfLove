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

	public override void Awake()
	{
		base.Awake();
		Init();
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
		SetCurrentDayBonus();
	}

	private void SetCurrentDayBonus()
	{
		BonusHolder currentBonus = null;
		foreach (var bonusHolder in _bonusHolders)
		{
			if(bonusHolder.Bonus.IsTake == false)
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
		_currentBonusDay.Take();
		SetCurrentDayBonus();
	}
	private List<Bonus> LoadBonuses()
	{
		var bonuses = Tools.BubbleSort(Resources.LoadAll<Bonus>("Bonuses"));
		return bonuses.ToList();
	}
}
