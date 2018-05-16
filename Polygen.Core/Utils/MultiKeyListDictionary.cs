using System;
using System.Collections.Generic;
using System.Linq;

namespace Polygen.Core.Utils
{
    /// <summary>
    /// Implements a dictionary which uses multiple keys and one key can hold multiple values.
    /// </summary>
    public class MultiKeyListDictionary<TValue>: Dictionary<ValueTuple<string, object>, List<TValue>>
    {
        public bool ContainsKey(string keyName, object key)
        {
            return GetList((keyName, key), false) != null;
        }
        
        public void Add(string keyName, object key, TValue value)
        {
            GetList((keyName, key), true).Add(value);;
        }
        
        public IEnumerable<TValue> GetOrEmpty(string keyName, object key)
        {
            return GetList((keyName, key), false) ?? Enumerable.Empty<TValue>();
        }

        public IEnumerable<TValue> GetOrNull(string keyName, object key)
        {
            return GetList((keyName, key), false);
        }
        
        public IEnumerable<TValue> GetOrCallback(string keyName, object key, Func<IEnumerable<TValue>> callback)
        {
            return GetList((keyName, key), false) ?? callback();
        }
        
        public IEnumerable<TValue> Get(string keyName, object key)
        {
            var internalKey = (keyName, key);

            return GetList(internalKey, false) ?? Enumerable.Empty<TValue>();
        }

        private List<TValue> GetList((string, object) internalKey, bool create)
        {
            if (TryGetValue(internalKey, out var list) || !create)
            {
                return list;
            }
            
            list = new List<TValue>();
            Add(internalKey, list);

            return list;
        }
        
    }
}