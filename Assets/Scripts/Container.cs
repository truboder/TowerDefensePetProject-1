using System;
using System.Collections.Generic;

public class Container
{
    private readonly Dictionary<Type, object> _services = new();

    private Container() { }

    public static Container Instance { get; } = new();

    public void Register<T>(T service)
    {
        _services[typeof(T)] = service;
    }

    public T Get<T>()
    {
        return _services.TryGetValue(typeof(T), out var service) ? (T)service : throw new Exception($"Service {typeof(T)} not found");
    }
}
