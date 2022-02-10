using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NotificationButton : MonoBehaviour
{
	[SerializeField] private TypeNotification _type;
	[SerializeField] private TMP_Text _countText;
	[SerializeField] private CanvasGroup _notificationCanvasGroup;
	private Coroutine _animationFader;
	[SerializeField] private bool _isAutoSaving = true;
	private int _countNotification = 0;

	public int CountNotification
	{
		get
		{
			if (_isAutoSaving)
			{
				return PlayerPrefs.GetInt($"Notification_{_type}", 0);
			}
			else
			{
				return _countNotification;
			}
			
		}
		set 
		{
			if (_isAutoSaving)
			{
				var count = PlayerPrefs.GetInt($"Notification_{_type}");
				count += value;

				if (count <= 0)
				{
					count = 0;
					HideNotification();
				}
				else ShowNotification();

				
				_countText.text = CountNotification.ToString();

				PlayerPrefs.SetInt($"Notification_{_type}", count);
			}
			else
			{
				_countNotification += value;
				if (_countNotification <= 0) HideNotification();
				else
				{
					_countText.text = _countNotification.ToString();
					ShowNotification();
				}
			}
			AudioManager.Instance.PlaySound(Sounds.Notification);
		}
	}

	public void ShowNotification()
	{
		if (_animationFader != null) StopCoroutine(_animationFader);

		_animationFader = StartCoroutine(Show());
	}
	public void HideNotification()
	{
		if (_animationFader != null) StopCoroutine(_animationFader);

		_animationFader = StartCoroutine(Hide());
	}

	private IEnumerator Show()
	{
		_notificationCanvasGroup.DOFade(1f, 1f);
		yield return new WaitForSeconds(1f);
	}
	private IEnumerator Hide()
	{
		_notificationCanvasGroup.DOFade(0f, 1f);
		yield return new WaitForSeconds(1f);
	}
}
