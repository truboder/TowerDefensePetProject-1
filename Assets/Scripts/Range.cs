using System;
using UnityEngine;

[Serializable]
public struct Range<T> where T : IComparable<T>
{
    [SerializeField] private T _min;
    [SerializeField] private T _max;

    public Range(T min, T max)
    {
        _min = min;
        _max = max;
    }

    public T Min => _min;
    public T Max => _max;

    public bool Contains(T value)
    {
        return value.CompareTo(_min) >= 0 && value.CompareTo(_max) <= 0;
    }
}
