using System;
using System.Collections.Generic;
using System.Linq;

namespace Polygen.Core.Utils
{
    /// <summary>
    /// Implements a dictionary which can hold multiple values for one key
    /// </summary>
    public class ListDictionary<TKey, TValue>: Dictionary<TKey, List<TValue>>
    {
        public void Add(TKey key, TValue value)
        {
            var list = GetList(key, true);
            
            list.Add(value);
        }
        
        public IEnumerable<TValue> GetOrEmpty(TKey key)
        {
            return GetList(key, false) ?? Enumerable.Empty<TValue>();
        }

        public IEnumerable<TValue> GetOrNull(TKey key)
        {
            return GetList(key, false);
        }

        private List<TValue> GetList(TKey key, bool create)
        {
            if (TryGetValue(key, out var list) || !create)
            {
                return list;
            }
            
            list = new List<TValue>();
            Add(key, list);

            return list;
        }
        
    }
}