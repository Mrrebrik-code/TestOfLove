using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ModeHolder : MonoBehaviour
{
	public ScrollObejct ScrollObject { get; private set; }
	[SerializeField] private Animator _animator;
	[SerializeField] private TMP_Text _priceText;
	public RectTransform RectTransform { get { return _rectTransform; } }
	[SerializeField] private RectTransform _rectTransform;
	[SerializeField] private TMP_Text _nameText;
	[SerializeField] private StatusMode _status;
	[SerializeField] private CategoryButton _categoryButton;
	[SerializeField] private Image _icon;

	[SerializeField] private GameObject[] _objectsToIcon;
	[SerializeField] private LockerScrollObject _lockObject;


	public void Init(ScrollObejct scrollObject)
	{
		ScrollObject = scrollObject;

		
		switch (scrollObject.TypeMode)
		{
			case TypeMode.VIPTestNames:
				_objectsToIcon[0].SetActive(true);
				_objectsToIcon[1].SetActive(false);
				_categoryButton.Init(scrollObject.TypeMode, "_ModeNames");
				break;
			case TypeMode.VIPNamesToTree:
				_objectsToIcon[0].SetActive(false);
				_objectsToIcon[1].SetActive(true); 
				_categoryButton.Init(scrollObject.TypeMode, "_ModeTree");
				break;
			case TypeMode.VIPScaner:
				_objectsToIcon[0].SetActive(false);
				_objectsToIcon[1].SetActive(false);
				_categoryButton.Init(scrollObject.TypeMode, "_ModeScaner");
				break;
		}
		_nameText.text = Localization.Instance.Localize(scrollObject.NameMode);
		Localization.Instance.Subscribe(() =>
		{
			_nameText.text = Localization.Instance.Localize(scrollObject.NameMode);
		});

		_status.SetStyle(scrollObject.Type);
		if(scrollObject.TypeMode == TypeMode.Default)
		{
			_categoryButton.Init(scrollObject.Category);
		}
		else
		{
			Locker(scrollObject.IsLock);
			_priceText.text = scrollObject.Price.ToString();
		}
		


		_icon.sprite = scrollObject.Icon;
	}

	public void ShowPopupLock()
	{
		_animator.SetTrigger("Show");
	}

	public void Locker(bool isLock)
	{
		if (isLock)
		{
			_lockObject.gameObject.SetActive(true);
		}
		else
		{
			_lockObject.UnLock();
			if(!PlayerPrefs.HasKey($"Saving_opening_vip_mode_{ScrollObject.TypeMode.ToString()}"))
			{
				PlayerPrefs.SetInt($"Saving_opening_vip_mode_{ScrollObject.TypeMode.ToString()}", 1);
				switch (ScrollObject.TypeMode)
				{
					case TypeMode.VIPNamesToTree:
						MailManager.Instance.AddLetter(Letters.OpenVIPModeTree, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "_Menu");
						break;
					case TypeMode.VIPScaner:
						MailManager.Instance.AddLetter(Letters.OpenVIPModeScaner, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "_Menu");
						break;
					case TypeMode.VIPTestNames:
						MailManager.Instance.AddLetter(Letters.OpenVIPModeTestOfNames, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "_Menu");
						break;
				}

			}
		}
		UpdateStatus();

		

	}
	public void UpdateStatus()
	{
		_status.SetStyle(ScrollObject.Type);
	}
}
