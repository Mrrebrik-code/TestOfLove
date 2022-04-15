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
		YandexSDK.Instance.onAuth += HandleAuthStatus;
		StartCoroutine(AuthStatus());
	}
	public void Init()
	{

		ResourcesManager.Instance.Initialization(() =>
		{
			StartCoroutine(Loading());
		});

	}

	public void Auth()
	{
		YandexSDK.Instance.Auth();
	}

	public void AuthGhost()
	{
		_isAuth = true;
	}

	private void HandleAuthStatus()
	{
		_isAuth = true;
	}

	private IEnumerator AuthStatus()
	{
		if (_isAuth == false)
		{
			_auth.gameObject.SetActive(true);
			_auth.DOFade(1f, 1f);
		}
		while (_isAuth == false)
		{
			yield return new WaitForEndOfFrame();
		}
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
