using TMPro;
using UnityEngine;

namespace Bank
{
	public class UIHeartsHolder : MonoBehaviour, IValueHolder
	{
		[SerializeField] private TMP_Text _cookiesCountText;

		public void UpdateText()
		{
			if(BankManager.Instance.Heart != null)
			{
				_cookiesCountText.text = BankManager.Instance.Heart.Count.ToString();
			}
			
		}
	}
}
