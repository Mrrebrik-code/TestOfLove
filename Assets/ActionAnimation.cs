using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionAnimation : MonoBehaviour
{
	[SerializeField] UnityEvent _event;

	public void Complet()
	{
		_event?.Invoke();
	}
}
