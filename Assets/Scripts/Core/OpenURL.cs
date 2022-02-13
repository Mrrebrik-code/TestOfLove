using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
	[SerializeField] private string _url;
	public void Open()
	{
		YandexSDK.Instance.OpenURL(_url);
	}
}
