using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public partial class LeaderHolder : MonoBehaviour
{
	private Leader _leader;
	[SerializeField] private TMP_Text _countText;
	[SerializeField] private TMP_Text _nameText;
	[SerializeField] private TMP_Text _valuesText;

	public void Init(Leader leader)
	{
		_leader = leader;
		_countText.text = leader.Id.ToString();
		_nameText.text = leader.Name;
		_valuesText.text = leader.Values.ToString();
	}
}
