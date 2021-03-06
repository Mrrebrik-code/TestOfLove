using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : MonoBehaviour
{
	private Categorys _category;
	private string _sceneName;
	private Button _button;
	private ModeHolder _modeHolder;

	public void Init(Categorys category)
	{
		_category = category;
		_modeHolder = GetComponent<ModeHolder>();
		_button = GetComponent<Button>();
		_button.onClick.AddListener(() =>
		{
			if (_modeHolder.ScrollObject.IsLock == false)
			{
				var successful = GameManager.SetCategory(_category, _modeHolder.ScrollObject);
				if (successful) SceneLoader.Instance.Load("_Game");
			}

		});
	}
	public void Init(TypeMode type, string nameScene)
	{
		_sceneName = nameScene;
		_modeHolder = GetComponent<ModeHolder>();
		_button = GetComponent<Button>();
		_button.onClick.AddListener(() =>
		{
			if (_modeHolder.ScrollObject.IsLock == false)
			{
				SceneLoader.Instance.Load(_sceneName);
			}
			else
			{
				if(_modeHolder.ScrollObject.Price > Bank.BankManager.Instance.Heart.Count)
				{
					_modeHolder.ShowPopupLock();
				}
				else
				{
					StackProductBuy.Instance.Init(_modeHolder.ScrollObject.Price, () =>
					{
						_modeHolder.ScrollObject.Locker(false);
						_modeHolder.Locker(false);
						_modeHolder.UpdateStatus();
					});
					WindowManager.Instance.HandleCurrentActiveWindow(Window.Popup_buy_mode);

				}
				
			}
		});
	}
}
