using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
	[SerializeField] private Sounds _type;

	private void Start()
	{
		var button = GetComponent<Button>();
		button.onClick.AddListener(() =>
		{
			AudioManager.Instance.PlaySound(_type);
		});
	}
}
