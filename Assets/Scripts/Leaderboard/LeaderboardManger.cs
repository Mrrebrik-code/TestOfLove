using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class LeaderboardManger : SingletonMono<LeaderboardManger>
{
	private const string _nameLeaderboard = "LiderboardLove";
	[SerializeField] private LeaderHolder _leaderHolderPrefab;
	[SerializeField] private Transform _content;
	private List<LeaderHolder> _leaders = new List<LeaderHolder>();
	private List<Leader> _leaderList = new List<Leader>();
	[SerializeField] private GameObject _noAuthText;
	

	private void Start()
	{
		if (YandexSDK.Instance.IsAuth)
		{
			_noAuthText.SetActive(false);
			Init();
			YandexSDK.Instance.onDataLeaderboardScorePlayerEntry += HandleDataLeaderboardScorePlayerEntry;
			YandexSDK.Instance.onDataLeaderboardsScoreTop += HandleDataLeaderboardsScoreTop;
		}
		else
		{
			_noAuthText.SetActive(true);
			StartCoroutine(UpdateAuthSystem());
		}
	}

	public void Auth()
	{
		YandexSDK.Instance.Auth();
	}
	private IEnumerator UpdateAuthSystem()
	{
		while (YandexSDK.Instance.IsAuth == false)
		{
			yield return null;
		}
		_noAuthText.SetActive(false);
		Init();
		YandexSDK.Instance.onDataLeaderboardScorePlayerEntry += HandleDataLeaderboardScorePlayerEntry;
		YandexSDK.Instance.onDataLeaderboardsScoreTop += HandleDataLeaderboardsScoreTop;
	}

	private void HandleDataLeaderboardsScoreTop(string data)
	{
		Debug.Log("Complet data parse");
		Debug.Log("Unity data: " + data);
		var json = JSON.Parse(data);
		var count = json["entries"].Count;

		CratingLeaderHolders(count, json["entries"]);
	}

	private void HandleDataLeaderboardScorePlayerEntry(string data)
	{
		var json = JSON.Parse(data);
		var score = (int)json["score"];
		var extraData = json["extraData"].ToString();
		var rank = (int)json["rank"];

		var player = new Player(json["player"]);

		var entries = new Entries(score, extraData, rank, player);
	}

	public void Init()
	{
		GetLeaderboards(4);
	}

	private void CratingLeaderHolders(int count, JSONNode json)
	{
		Entries[] entries = new Entries[count];
		for (int i = 0; i < count; i++)
		{
			var score = (int)json[i]["score"];
			var rank = (int)json[i]["rank"];
			var extraData = json[i]["extraData"].ToString();
			var player = new Player(json[i]["player"]);

			var entrie = new Entries(score, extraData, rank, player);

			entries[i] = entrie;
		}

		var eaderboardEntries = new LeaderboardEntries(entries);


		for (int i = 0; i < eaderboardEntries.entries.Length; i++)
		{
			var score = eaderboardEntries.entries[i].score;
			var name = eaderboardEntries.entries[i].player.publicName;

			_leaderList.Add(new Leader(i + 1, name, score));
		}

		foreach (var leader in _leaderList)
		{
			var leaderHolder = Instantiate(_leaderHolderPrefab, _content);
			leaderHolder.Init(leader);
			_leaders.Add(leaderHolder);
		}

	}


	public void SetLeaderboardScore(int score, string description)
	{
		if (YandexSDK.Instance.IsAuth)
		{
			YandexSDK.Instance.SetLeaderboardScore(_nameLeaderboard, score, description);
		}
	}

	public void GetLeaderboard()
	{
		if (YandexSDK.Instance.IsAuth)
		{
			YandexSDK.Instance.GetLeaderboardScorePlayerEntry(_nameLeaderboard);
		}
	}

	public void GetLeaderboards(int countTopPlayers)
	{
		if (YandexSDK.Instance.IsAuth)
		{
			YandexSDK.Instance.GetLeaderboardsScoreTop(_nameLeaderboard, countTopPlayers);
		}
	}

	public void UpdateLeaderboard()
	{
		if (YandexSDK.Instance.IsAuth)
		{
			_leaders.ForEach(leaderHolder =>
			{
				Destroy(leaderHolder.gameObject);
			});

			_leaders = new List<LeaderHolder>();
			_leaderList = new List<Leader>();

			Init();
		}
	}

	[System.Serializable]
	public class LeaderboardPlayer
	{
		public int score;
		public string extraData;

		public LeaderboardPlayer(int score, string extraData)
		{
			this.score = score;
			this.extraData = extraData;
		}

	}

	[System.Serializable]
	public class LeaderboardEntries
	{
		public Entries[] entries;

		public LeaderboardEntries(Entries[] entries)
		{
			this.entries = entries;
		}
	}

	[System.Serializable]
	public class Player
	{
		public string publicName;

		public Player(JSONNode json)
		{
			publicName = json["publicName"].ToString();
		}
	}

	[System.Serializable]
	public class Entries
	{
		public int score;
		public string extraData;
		public int rank;

		public Player player;

		public Entries(int score, string extraData, int rank, Player player)
		{
			this.score = score;
			this.extraData = extraData;
			this.rank = rank;
			this.player = player;
		}
	}
}
