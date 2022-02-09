using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockerScrollObject : MonoBehaviour
{
	[SerializeField] private Animator _animator;


	public void EndAniamtionUnLock()
	{
		gameObject.SetActive(false);
	}
	public void UnLock()
	{
		_animator.SetTrigger("UnLock");
	}
}
