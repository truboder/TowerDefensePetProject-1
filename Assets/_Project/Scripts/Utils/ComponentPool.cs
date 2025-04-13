using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ComponentPool<T> where T : Component
{
    private readonly Stack<T> _pool = new Stack<T>();
    private readonly T _prefab;
    private readonly Transform _parent;
    private readonly DiContainer _container;


    public ComponentPool(T prefab, DiContainer container, Transform parent = null)
    {
        _prefab = prefab;
        _container = container;
        _parent = parent;
    }

    public T Get()
    {
        if (_pool.Count > 0)
        {
            T obj = _pool.Pop();
            obj.gameObject.SetActive(true);
            return obj;
        }

        return _container.InstantiatePrefabForComponent<T>(_prefab, _parent);
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Push(obj);
    }
}
