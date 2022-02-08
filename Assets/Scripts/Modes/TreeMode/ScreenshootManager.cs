using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
public class ScreenshootManager : MonoBehaviour
{
	[SerializeField] private GameObject[] _objectsOff;
	[SerializeField] private GameObject[] _objectsOn;
	[SerializeField] private TMP_InputField _inputName1;
	[SerializeField] private TMP_InputField _inputName2;
	[SerializeField] private TMP_Text _namesText;
	[SerializeField] private GameObject _screenTree;
	[SerializeField] private GameObject _screenCreating;
	[DllImport("__Internal")] private static extern void DownloadFile(byte[] array, int byteLength, string fileName);
	public void Download()
	{


		StartCoroutine(TakeScreenShot());
		/*		byte[] textureBytes = texture.EncodeToPNG();
				DownloadFile(textureBytes, textureBytes.Length, "screenshot.png");
				Destroy(texture);*/
	}

	public void Create()
	{
		_namesText.text = $"{_inputName1.text}\n+\n{_inputName2.text}";
		_inputName1.text = null;
		_inputName2.text = null;
		_screenTree.gameObject.SetActive(true);
		_screenCreating.gameObject.SetActive(false);
	}

	IEnumerator TakeScreenShot()
	{
		foreach (var off in _objectsOff) off.SetActive(false);
		foreach (var on in _objectsOn) on.SetActive(true);
		yield return new WaitForEndOfFrame();
		ScreenCapture.CaptureScreenshot("Test.png");
		foreach (var off in _objectsOff) off.SetActive(true);
		foreach (var on in _objectsOn) on.SetActive(false);

	}
}
