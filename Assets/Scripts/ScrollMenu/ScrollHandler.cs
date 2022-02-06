using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollHandler : SingletonMono<ScrollHandler>
{
	[SerializeField] private List<ScrollObejct> _scrollObjects;
	[SerializeField] private Transform _content;
	[SerializeField] private List<ModeHolder> _modesHodler = new List<ModeHolder>();
	[SerializeField] private List<Vector2> _positions = new List<Vector2>();
	[SerializeField] private float _spacing;
	[SerializeField] private float _smooth;
	private Vector2 _vectorPosition;
	private bool _isScrolling;
	private int test = 0;
	public override void Awake()
	{
		base.Awake();
		Initialization();
	}

	private List<ScrollObejct> LoadScrollObjcts()
	{
		var objects = Resources.LoadAll<ScrollObejct>("ScrollObjects");
		Tools.BubbleSort(objects);
		return objects.ToList();
	}

	private void Initialization()
	{
		_scrollObjects = LoadScrollObjcts();
		if (_scrollObjects == null) return;

		for (int i = 0; i < _scrollObjects.Count; i++)
		{
			var mode = Instantiate(_scrollObjects[i].ModePrefab, _content, false);
			mode.Init(_scrollObjects[i]);
			_modesHodler.Add(mode);

			if (i != 0)
			{
				ModeHolder modeHolderOld = _modesHodler[i - 1];
				var deltaOld = (modeHolderOld.RectTransform.sizeDelta.x / 2);
				var delta = (mode.RectTransform.sizeDelta.x / 2);

					Vector2 position = new Vector2(modeHolderOld.RectTransform.anchoredPosition.x + deltaOld + delta + _spacing, mode.transform.localPosition.y);
				
					/*new Vector2(modeHolderOld.transform.localPosition.x + mode.GetComponent<RectTransform>().sizeDelta.x + _spacing, mode.transform.localPosition.y);*/
				mode.transform.localPosition = position;
				_positions.Add(-mode.transform.localPosition);
			}
			else
			{
				_positions.Add(-mode.transform.localPosition);
			}

		}
	}

	private void FixedUpdate()
	{
		float nearestPosition = float.MaxValue;
		for (int i = 0; i < _positions.Count; i++)
		{
			float distance = Mathf.Abs(_content.GetComponent<RectTransform>().anchoredPosition.x - _positions[i].x);
			if(distance < nearestPosition)
			{
				nearestPosition = distance;
				test = i;
			}
		}
		if (_isScrolling) return;
		_vectorPosition.x = Mathf.SmoothStep(_content.GetComponent<RectTransform>().anchoredPosition.x, _positions[test].x, _smooth * Time.fixedDeltaTime);
		_content.GetComponent<RectTransform>().anchoredPosition = _vectorPosition;
	}

	public void Scrolling(bool isScroll)
	{
		_isScrolling = isScroll;
	}
}
