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
	[SerializeField] private Image _notificationIcon;
	private Coroutine _animationFader;
	[SerializeField] private int _maxCount;
	private int CountNotification
	{
		get => PlayerPrefs.GetInt($"Notification_{_type}", 0);
		set => PlayerPrefs.SetInt($"Notification_{_type}", value);
	}

	public void ShowNotification(bool isAdd = true)
	{
		if(isAdd) CountNotification++;
		if (CountNotification >= _maxCount) CountNotification = _maxCount;
		_countText.text = CountNotification.ToString();
		_countText.DOFade(1, 1f);
		_animationFader = StartCoroutine(Show());
	}
	public void HideNotification()
	{
		CountNotification--;
		if(CountNotification < 0)
		{
			CountNotification = 0;
		}

		_countText.text = CountNotification.ToString();
		if(CountNotification <= 0)
		{
			_countText.DOFade(0, 1f);
			_animationFader = StartCoroutine(Hide());
		}
		
	}

	private IEnumerator Show()
	{
		_notificationIcon.DOFade(1f, 1f);
		yield return new WaitForSeconds(1f);
	}
	private IEnumerator Hide()
	{
		_notificationIcon.DOFade(0f, 1f);
		yield return new WaitForSeconds(1f);
	}
}
