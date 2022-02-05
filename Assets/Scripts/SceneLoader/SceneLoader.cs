using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : SingletonMono<SceneLoader>
{
	public void Load(string name)
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(name);
	}
}
