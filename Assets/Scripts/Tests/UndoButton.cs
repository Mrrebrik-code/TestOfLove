using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UndoButton : MonoBehaviour
{
	private Button _button;
	private void Awake()
	{
		_button = GetComponent<Button>();
		YandexSDK.Instance.onRewardedAdReward += Undo;
		_button.onClick.AddListener(() =>
		{
			ShowReward();
		});
	}
	private void OnDestroy()
	{
		YandexSDK.Instance.onRewardedAdReward -= Undo;
	}
	private void Undo(string reward)
	{
		if(reward == "Back") TestHandler.Instance.Undo();
	}

	private void ShowReward()
	{
		if (DeviceManager.Instance._isUnity)
		{
			Undo("Back");
		}
		else
		{
			YandexSDK.Instance.ShowRewarded("Back");
		}
		
	}
}

