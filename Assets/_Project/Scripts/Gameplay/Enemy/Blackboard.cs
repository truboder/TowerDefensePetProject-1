using System.Collections.Generic;

namespace Gameplay.Enemy
{
    public class Blackboard
    {
        private readonly Dictionary<string, object> _storage = new();

        public bool TryGetData(string key, out object data)
        {
            data = null;
            return _storage.TryGetValue(key, out data);
        }
        
        public bool TryGetData<T>(string key, out T data)
        {
            data = default;
            
            if (!_storage.TryGetValue(key, out object rawData))
                return false;

            data = (T) rawData;
            
            return true;
        }

        public bool TrySetData(string key, object data)
        {
            if (_storage.ContainsKey(key))
                return false;

            _storage.Add(key, data);
            return true;
        }

        public bool TryClearData(string key)
        {
            return _storage.Remove(key);
        }
    }
}