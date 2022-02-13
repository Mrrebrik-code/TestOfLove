using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoAuthHolder : MonoBehaviour
{
	private void Start()
	{
		if(YandexSDK.Instance.IsAuth == false)
		{
			StartCoroutine(TimeHOlder());
		}
	}

	private IEnumerator TimeHOlder()
	{
		while (YandexSDK.Instance.IsAuth == false)
		{
			yield return null;
		}
		Destroy(gameObject);
	}
}
