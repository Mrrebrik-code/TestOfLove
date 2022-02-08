using System.Collections.Generic;
using UnityEngine;

namespace Bank
{
	public class HistoryTransaction
	{
		private List<Transaction> _transactions = new List<Transaction>();
		public void EnqueueTransaction(Transaction transaction)
		{
			_transactions.Add(transaction);
		}

		public Transaction PeekTransaction()
		{
			return _transactions[_transactions.Count];
		}

		public Transaction DequeueTransaction()
		{
			var temp = _transactions[_transactions.Count - 1];
			_transactions.Remove(temp);

			return temp;
		}

		public void UndoTransaction()
		{
			if(_transactions.Count > 0)
			{
				var transaction = DequeueTransaction();
				switch(transaction.Type)
				{
					case TypeTransaction.Heart:
						BankManager.Instance.Heart.Put(transaction.Price);
						Debug.LogError("Вернули: " + transaction.Price);
						break;
				}
			}
		}
	}
}
