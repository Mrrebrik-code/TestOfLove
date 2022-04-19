using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankSave : ISaver
{
	public int countHeart = default;

	public BankSave() { }
	public BankSave(int countHeart)
	{
		this.countHeart = countHeart;
	}
	
}
