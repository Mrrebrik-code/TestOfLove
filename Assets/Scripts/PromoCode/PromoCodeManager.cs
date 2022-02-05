using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoCodeManager : SingletonMono<PromoCodeManager>
{
	public const string Love10 = "Love10";
	public const string Love100 = "Love100";
	public const string Love1000 = "Love1000";

	public void PromoCodoActivate(string code)
	{
		switch (code)
		{
			case Love10:
				Debug.Log("10");
				break;
			case Love100:
				Debug.Log("100");
				break;
			case Love1000:
				Debug.Log("1000");
				break;
		}
	}
}
