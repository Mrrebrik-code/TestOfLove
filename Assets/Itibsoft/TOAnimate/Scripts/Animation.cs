using System;
using UnityEngine;
using UnityEngine.UI;

namespace Itibsoft.TOAnimate 
{
	public static class Animation
	{
		public static void TOAFade(this Image image, float target, float time, Action callback)
		{
			TOAnimate.Instance.Fade(image, image.color.a, target, time, callback);
		}

		public static void TOAFade(this CanvasGroup canvasGroup, float target, float time, Action callback)
		{
			TOAnimate.Instance.Fade(canvasGroup, canvasGroup.alpha, target, time, callback);
		}

		public static void TOAMove(this RectTransform recTransform, Vector2 target, float time, Action callback)
		{
			TOAnimate.Instance.Move(recTransform, recTransform.anchoredPosition, target, time, callback);
		}

		public static void TOAColor(this Image image, Color target, float time, Action callback)
		{
			TOAnimate.Instance.Color(image, image.color, target, time, callback);
		}
	}
}


