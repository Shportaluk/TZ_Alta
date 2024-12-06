using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameFuncs
{
    public static IEnumerable<T> FilterByDistance<T>(Vector3 sourcePosition, IEnumerable<T> elements, float maxDist)
        where T : MonoBehaviour
    {
        return FilterBy(elements, Check);

        bool Check(T item)
        {
            var dist = Vector3.Distance(sourcePosition, item.transform.position);
            return dist <= maxDist;
        }
    }

    public static IEnumerable<T> FilterBy<T>(IEnumerable<T> elements, Func<T, bool> func)
    {
        foreach (var item in elements)
        {
            if (func(item))
            {
                yield return item;
            }
        }
    }

    public static void ForEach<T>(this IEnumerable<T> elements, Func<T, Action> func)
    {
        foreach (var item in elements)
        {
            func(item)();
        }
    }

    public static Coroutine DoAction(this MonoBehaviour mb, float time, Action action)
    {
        return mb.StartCoroutine(Cor());

        IEnumerator Cor()
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}