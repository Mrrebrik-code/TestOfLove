using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoaderButton : MonoBehaviour
{
	[SerializeField] private string _nameScene;
	private Button _button;
	private void Awake()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(() =>
		{
			SceneLoader.Instance.Load(_nameScene);
		});
	}
}
