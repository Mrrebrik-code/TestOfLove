using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollHandler : SingletonMono<ScrollHandler>
{
	[SerializeField] private List<ScrollObejct> _scrollObjects;
	[SerializeField] private Transform _content;

	[SerializeField] private List<ModeHolder> _modesHodler = new List<ModeHolder>();
	[SerializeField] private float _spacing;

	public override void Awake()
	{
		base.Awake();
		Initialization();
	}

	private List<ScrollObejct> LoadScrollObjcts()
	{
		var objects = Resources.LoadAll<ScrollObejct>("ScrollObjects");
		return objects.ToList();
	}

	private void Initialization()
	{
		_scrollObjects = LoadScrollObjcts();
		if (_scrollObjects == null) return;

		for (int i = 0; i < _scrollObjects.Count; i++)
		{
			var mode = Instantiate(_scrollObjects[i].ModePrefab, _content);
			mode.Init(_scrollObjects[i]);
			_modesHodler.Add(mode);

			if (i != 0)
			{
				ModeHolder modeHolderOld = _modesHodler[i - 1];
				Vector2 position = new Vector2(modeHolderOld.RectTransform.anchoredPosition.x + _spacing, mode.RectTransform.anchoredPosition.y);
				mode.RectTransform.anchoredPosition = position;
			}

		}
		
	}
}
