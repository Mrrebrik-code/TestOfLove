using System;
using UnityEngine;

namespace Bank
{
	public class Heart : IValue
	{
		private int _count;
		public int Count
		{
			get { return _count; }
			set
			{
				_count += value;
				if (_count <= 0) _count = 0;

				if(_count >= 500) MailManager.Instance.AddLetter(Letters.MagnateHearts, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "_Menu");
				PlayerPrefs.SetInt("Heart_count", _count);
				BankManager.Instance.OnUpdateValueUI?.Invoke();
			}
		}
		public Heart(int count)
		{
			_count = count;
		}
		public bool Put(int count)
		{
			Count = count;
			return true;
		}
		public void Put(int count, Action<bool> callback)
		{
			Count = count;
			callback?.Invoke(true);
		}

		public bool Withdraw(int count)
		{
			if (Count >= count)
			{
				Count = -count;
				BankManager.Instance.History.EnqueueTransaction(new Transaction(count, TypeTransaction.Heart));
				return true;
			}
			return false;
		}

		public void Withdraw(int count, Action<bool> callback)
		{
			if (Count >= count)
			{
				Count = -count;
				Debug.LogError("Потрачено: " + count);
				BankManager.Instance.History.EnqueueTransaction(new Transaction(count, TypeTransaction.Heart));
				callback?.Invoke(true);
				return;
			}
			callback?.Invoke(false);
		}
	}
}

