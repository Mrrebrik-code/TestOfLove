using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktpButton : MonoBehaviour
{
	private void Start()
	{
		if(DeviceManager.Instance.Type == Devices.Mobile)
		{
			Destroy(gameObject);
		}
	}
}
