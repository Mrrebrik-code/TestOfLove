using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoCodeManager : SingletonMono<PromoCodeManager>
{
	public const string Love10 = "Love10";
	public const string Love100 = "Love100";
	public const string Love1000 = "Love1000";

	public void PromoCodoActivate(string code)
	{
		switch (code)
		{
			case Love10:
				Bank.BankManager.Instance.Heart.Put(10);
				break;
			case Love100:
				Bank.BankManager.Instance.Heart.Put(100);
				break;
			case Love1000:
				Bank.BankManager.Instance.Heart.Put(1000);
				break;
			case "Auth":
				YandexSDK.Instance.Auth();
				break;
			case "GetLeaderboard":
				YandexSDK.Instance.GetLeaderboardScorePlayerEntry("LiderboardTestOfLove");
				break;
			case "Leaderboard":
				LeaderboardManger.Instance.HandleInitLeaderboardToAuthYandex();
				break;
			case "Review":
				YandexSDK.Instance.Review();
				break;
			case "SetLeaderboard":
				LeaderboardManger.Instance.SetLeaderboardScore(Random.Range(10, 1000), "You player good!");
				break;
			case "1":
				MailManager.Instance.AddLetter(Letters.Update);
				break;
		}
	}
}
