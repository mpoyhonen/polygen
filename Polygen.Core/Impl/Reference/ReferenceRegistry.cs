using System;
using System.Collections.Generic;
using System.Linq;
using Polygen.Core.Exceptions;
using Polygen.Core.Reference;
using Polygen.Core.Utils;

namespace Polygen.Core.Impl.Reference
{
    /// <inheritdoc />
    public class ReferenceRegistry : IReferenceRegistry
    {
        /// <summary>
        /// Contains the registed references mapped by the target object name and type. 
        /// </summary>
        private readonly ListDictionary<(string, Type), object> _referenceMap = new ListDictionary<(string, Type), object>();        
        /// <summary>
        /// Contains the registed target objects mapped by the target object name and type. 
        /// </summary>
        private readonly Dictionary<(string, Type), object> _targetObjectMap = new Dictionary<(string, Type), object>();

        /// <inheritdoc />
        public IReference<T> AddReference<T>(IReference<T> reference)
        {
            var key = (reference.Name, typeof(T));

            if (_targetObjectMap.TryGetValue(key, out var targetObject))
            {
                // Target object already registered, so fill that in the reference.
                reference.Target = (T) targetObject;
            }
            
            _referenceMap.Add(key, reference);

            return reference;
        }

        /// <inheritdoc />
        public void RegisterObject<T>(string name, T obj)
        {
            var key = (name, typeof(T));

            if (_targetObjectMap.ContainsKey(key))
            {
                throw new ConfigurationException($"Target object '{name}' with type '{typeof(T)}' is already registered.");
            }

            // Update all references.
            foreach (var reference in _referenceMap.GetOrEmpty(key).Cast<IReference<T>>())
            {
                reference.Target = (T) obj;
            }
        }
    }
}