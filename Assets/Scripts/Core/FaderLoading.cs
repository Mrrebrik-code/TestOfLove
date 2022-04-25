using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FaderLoading : MonoBehaviour
{
	[SerializeField] private Image _image;
	[SerializeField] private CanvasGroup _canvasGroup;

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}
	public void Show(string nameScene)
	{
		_image.DOFade(1, 1f);
		StartCoroutine(Updater(nameScene));
	}
	private IEnumerator Updater(string nameScene)
	{
		while (true)
		{
			yield return new WaitForEndOfFrame();
			if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == nameScene)
			{
				Hide();
			}
		}
	}
	public void Hide()
	{
		_image.DOFade(0, 1f).onComplete += () =>
		{
			_canvasGroup.DOFade(0, 1f).onComplete += () =>
			{
				DestroyImmediate(gameObject);
			};
		};
		
	}
}
