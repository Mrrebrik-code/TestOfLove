using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSDK : SingletonMono<YandexSDK> 
{
    [DllImport("__Internal")] private static extern void ShowFullscreenAd();
    [DllImport("__Internal")] private static extern int ShowRewardedAd(string placement);
    [DllImport("__Internal")] private static extern void GerReward();
    [DllImport("__Internal")] private static extern void AuthenticateUser();
    [DllImport("__Internal")] private static extern void InitPurchases();
    [DllImport("__Internal")] private static extern void Purchase(string id);
    [DllImport("__Internal")] private static extern void CanReviewGame();
    [DllImport("__Internal")] private static extern void SetLeaderboard(string name, int count, string description);
    [DllImport("__Internal")] private static extern void GetLeaderboard(string name);
    [DllImport("__Internal")] private static extern void GetLeaderboards(string name, int count);



    public event Action onInterstitialShown;
    public event Action<string> onInterstitialFailed;
    public event Action<int> onRewardedAdOpened;
    public event Action<string> onRewardedAdReward;
    public event Action<int> onRewardedAdClosed;
    public event Action<string> onRewardedAdError;

    public event Action<string> onDataLeaderboardScorePlayerEntry;
    public event Action<string> onDataLeaderboardsScoreTop;

    public event Action onAuth;

    public Queue<int> rewardedAdPlacementsAsInt = new Queue<int>();
    public Queue<string> rewardedAdsPlacements = new Queue<string>();

	public override void Awake()
	{
		base.Awake();
        
    }

    public void Auth()
	{
        AuthenticateUser();
    }

    public void AuthorizationStatus(string name)
	{
        Debug.Log("Auth succesfyl");
        onAuth?.Invoke();
    }


	public void SetLeaderboardScore(string name, int score, string description)
	{
        SetLeaderboard(name, score, description);
	}

    public void GetLeaderboardScorePlayerEntry(string name)
    {
        GetLeaderboard(name);
    }

    public void GetLeaderboardsScoreTop(string name, int countTopPlayers)
    {
        GetLeaderboards(name, countTopPlayers);
    }

    public void OnLeaderboardsScoreTop(string data)
	{
        onDataLeaderboardsScoreTop(data);

    }

    public void OnLeaderboardScorePlayerEntry(string data)
    {
        onDataLeaderboardScorePlayerEntry(data);
    }

    public void ShowInterstitial() 
    {
        ShowFullscreenAd();
    }

    public void Review()
	{
        CanReviewGame();
    }

    public void ShowRewarded(string placement) 
    {
        rewardedAdPlacementsAsInt.Enqueue(ShowRewardedAd(placement));
        rewardedAdsPlacements.Enqueue(placement);
    }
 
    public void OnInterstitialShown() 
    {
        onInterstitialShown();
    }

    public void OnInterstitialError(string error) 
    {
        onInterstitialFailed(error);
    }

    public void OnRewardedOpen(int placement) 
    {
        onRewardedAdOpened(placement);
    }

    public void OnRewarded(int placement) 
    {
        if (placement == rewardedAdPlacementsAsInt.Dequeue()) 
        {
            onRewardedAdReward.Invoke(rewardedAdsPlacements.Dequeue());
        }
    }

    public void OnRewardedClose(int placement) 
    {
        onRewardedAdClosed(placement);
    }

    public void OnRewardedError(string placement) 
    {
        onRewardedAdError(placement);
        rewardedAdsPlacements.Clear();
        rewardedAdPlacementsAsInt.Clear();
    }

}
