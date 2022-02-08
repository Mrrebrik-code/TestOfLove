using System.Collections.Generic;
using UnityEngine;

namespace Bank
{
	public class UIBankManager : MonoBehaviour
	{
		[SerializeField] private UIHeartsHolder _heartsHolder;
		private List<IValueHolder> _holders = new List<IValueHolder>();
		public void Awake()
		{
			_holders.Add(_heartsHolder);

			BankManager.Instance.OnUpdateValueUI += OnUpdateHandler;
			OnUpdateHandler();
		}

		private void OnUpdateHandler()
		{
			_holders.ForEach(holder => holder.UpdateText());
		}
	}
}
