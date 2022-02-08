using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Itibsoft.TOAnimate
{
	public class TOAnimate : SingletonMono<TOAnimate>
	{
		#region Fade
		public void Fade(Image image, float start, float target, float time, Action callback)
		{
			StartCoroutine(DOFadeCoroutine(image, start, target, time, callback));
		}
		public void Fade(CanvasGroup canvasGroup, float start, float target, float time, Action callback)
		{
			StartCoroutine(DOFadeCoroutine(canvasGroup, start, target, time, callback));
		}

		private IEnumerator DOFadeCoroutine(Image image, float start, float target, float time, Action callback)
		{
			var isLogic = false;
			var alpha = start;

			if (start > target) isLogic = false;
			else isLogic = true;

			while (image.color.a != target)
			{
				if (isLogic) alpha += Time.deltaTime * time;
				else alpha -= Time.deltaTime * time;

				var alphaTemp = 0f;
				if (isLogic) alphaTemp = Mathf.Clamp(alpha, start, target);
				else alphaTemp = Mathf.Clamp(alpha, target, start);

				image.color = new Color(image.color.r, image.color.g, image.color.b, alphaTemp);
				yield return null;
			}
			callback?.Invoke();
		}

		private IEnumerator DOFadeCoroutine(CanvasGroup canvas, float start, float target, float time, Action callback)
		{
			var isLogic = false;
			var alpha = start;

			if (start > target) isLogic = false;
			else isLogic = true;

			while (canvas.alpha != target)
			{
				if (isLogic) alpha += Time.deltaTime * time;
				else alpha -= Time.deltaTime * time;

				var alphaTemp = 0f;
				if (isLogic) alphaTemp = Mathf.Clamp(alpha, start, target);
				else alphaTemp = Mathf.Clamp(alpha, target, start);

				canvas.alpha = alphaTemp;
				yield return null;
			}
			callback?.Invoke();
		}
		#endregion
		#region Move
		public void Move(RectTransform recTransform, Vector2 start, Vector2 target, float time, Action callback)
		{
			StartCoroutine(DOMoveCoroutine(recTransform, start, target, time, callback));
		}
		private IEnumerator DOMoveCoroutine(RectTransform recTransform, Vector2 start, Vector2 target, float time, Action callback)
		{

			while (Vector2.Distance(recTransform.anchoredPosition, target) > float.Epsilon)
			{
				recTransform.anchoredPosition = Vector2.MoveTowards(recTransform.anchoredPosition, target, time);
				yield return null;
			}
			callback?.Invoke();
		}
		#endregion

		#region Color
		public void Color(Image image, Color start, Color target, float time, Action callback )
		{
			StartCoroutine(DOColorCoroutine(image, start, target, time, callback));
		}

		private IEnumerator DOColorCoroutine(Image image, Color start, Color target, float time, Action callback)
		{
			var startColor = start;
			while (image.color != target)
			{
				startColor = UnityEngine.Color.Lerp(startColor, target, time);
				image.color = startColor;
				yield return null;
			}
			callback?.Invoke();
		}

		#endregion
	}
}
