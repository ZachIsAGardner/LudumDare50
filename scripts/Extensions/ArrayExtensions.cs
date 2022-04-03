using System.Collections.Generic;
using System.Linq;
using Godot;
using Godot.Collections;

public static class ArrayExtensions
{
    public static T Random<T>(this List<T> list)
    {
        RandomNumberGenerator rng = new RandomNumberGenerator();
        rng.Randomize();
        int r = rng.RandiRange(0, list.Count() - 1);
        return list[r];
    }

    public static List<T> ToList<T>(this Array array)
    {
        List<T> list = new List<T>() { };
        foreach (T item in array) list.Add(item);
        return list;
    }
    // public static IEnumerable<T> ToIEnumerable<T>(this Array array)
    // {
    //     return (IEnumerable<T>)array;
    // }

    // public static Array<T> ToArray<T>(this IEnumerable<T> collection)
    // {
    //     return (Array<T>)collection;
    // }
}