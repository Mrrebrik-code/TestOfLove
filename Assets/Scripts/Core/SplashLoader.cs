using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SplashLoader : MonoBehaviour
{
	[SerializeField] private string _nameSceneToLoad;
	[SerializeField] private Animator _logo;
	[SerializeField] private FaderLoading _fader;
	[SerializeField] private CanvasGroup _auth;
	[SerializeField] private bool _isAuth;

	private void Start()
	{
		StartCoroutine(AuthStatus());
	}
	public void Init()
	{

		ResourcesManager.Instance.Initialization(() =>
		{
			StartCoroutine(Loading());
		});

	}

	private IEnumerator AuthStatus()
	{
		yield return new WaitForSeconds(1f);
		_auth.DOFade(0f, 1f).onComplete += () =>
		{
			_logo.SetTrigger("LogoStart");
			_auth.gameObject.SetActive(false);
		};
	}

	private IEnumerator Loading()
	{

		var progressive = SceneManager.LoadSceneAsync(_nameSceneToLoad);
		while(progressive.progress < 0.2f) { yield return new WaitForEndOfFrame(); }
		_fader.Show(_nameSceneToLoad);

		
	}

}
