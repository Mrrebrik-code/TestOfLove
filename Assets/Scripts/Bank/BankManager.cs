using System;
using UnityEngine;

namespace Bank
{
	public class BankManager : SingletonMono<BankManager>
	{
		public Action OnUpdateValueUI;
		public Heart Heart { get; private set; }
		public HistoryTransaction History { get; private set; }
		public override void Awake()
		{
			base.Awake();
			History = new HistoryTransaction();

			if (SaveManager.Bank != null)
			{
				var count = SaveManager.Bank.countHeart;
				Heart = new Heart(count);
			}
			else
			{
				Heart = new Heart(10);
			}

			OnUpdateValueUI?.Invoke();
		}
	}
}


