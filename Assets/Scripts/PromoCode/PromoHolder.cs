using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromoHolder : MonoBehaviour
{
	[SerializeField] private TMP_InputField _input;

	public void EnterCode()
	{
		PromoCodeManager.Instance.PromoCodoActivate(_input.text);
		_input.text = null;
	}

}
