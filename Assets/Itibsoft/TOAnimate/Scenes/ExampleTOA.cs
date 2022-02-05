using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Itibsoft.TOAnimate;

public class ExampleTOA : MonoBehaviour
{
	[SerializeField] private Image _image;
	private bool _isFadering = false;
	private bool _isMoving = false;
	private bool _isColoring = false;
	private bool _isLeft = false;
	[SerializeField] private RectTransform[] _positions;
	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && _isFadering == false)
		{
			_isFadering = true;
			_image.TOAFade(_image.color.a == 1 ? 0 : 1, 1f, () => 
			{ 
				Debug.Log("Animation TOAFade to image complet!");
				_isFadering = false;
			});
		}
		if (Input.GetKeyDown(KeyCode.A) && _isMoving == false)
		{
			_isMoving = true;
			_image.rectTransform.TOAMove(_isLeft ? _positions[0].localPosition : _positions[1].localPosition, 5f, () =>
			{
				Debug.Log("Animation TOAMove to image complet!");
				_isLeft = !_isLeft;
				_isMoving = false;
			});
		}
		if (Input.GetKeyDown(KeyCode.D) && _isColoring == false)
		{
			_isColoring = true;
			_image.TOAColor(_image.color == Color.green ? Color.red : Color.green, 0.02f, () =>
			{
				_isColoring = false;
			});
		}
	}
}
