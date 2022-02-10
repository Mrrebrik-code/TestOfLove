using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLoader : MonoBehaviour
{
	[SerializeField] private FaderLoading _fader;

	public void Init()
	{
		ResourcesManager.Instance.Initialization(() =>
		{
			StartCoroutine(Loading());
		});
		
	}

	private IEnumerator Loading()
	{

		var progressive = SceneManager.LoadSceneAsync("_Menu");
		while(progressive.progress < 0.2f) { yield return new WaitForEndOfFrame(); }
		_fader.Show();

		
	}

}
