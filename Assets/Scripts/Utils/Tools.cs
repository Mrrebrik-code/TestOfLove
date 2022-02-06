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

    //метод обмена элементов
    private static void Swap(ref ScrollObejct e1, ref ScrollObejct e2)
    {
        var temp = e1;
        e1 = e2;
        e2 = temp;
    }

    //сортировка пузырьком
    public static ScrollObejct[] BubbleSort(ScrollObejct[] array)
    {
        var len = array.Length;
        for (var i = 1; i < len; i++)
        {
            for (var j = 0; j < len - i; j++)
            {
                if (array[j].Id > array[j + 1].Id)
                {
                    Swap(ref array[j], ref array[j + 1]);
                }
            }
        }

        return array;
    }
}
