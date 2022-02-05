using System;
using System.Collections.Generic;
using UnityEngine.UI;

public static class Tools
{
	public static void ListenerButton(this Button button, UnityEngine.Events.UnityAction call, bool isRemove = false)
	{
		if (isRemove) button.onClick.RemoveAllListeners();

		button.onClick.AddListener(call);
	}

    public static void RandomSorting<T>(this List<T> list)
    {
        Random rand = new Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            T tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }
    }
}
