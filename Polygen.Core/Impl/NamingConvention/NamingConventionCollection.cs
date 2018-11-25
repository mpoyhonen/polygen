using System;
using System.Collections.Generic;
using System.Text;
using Polygen.Core.Exceptions;
using Polygen.Core.NamingConvention;

namespace Polygen.Core.Impl.NamingConvention
{
    public class NamingConventionCollection : INamingConventionCollection
    {
        private readonly Dictionary<string, INamingConvention> _namingConventionMap = new Dictionary<string, INamingConvention>();
        
        public NamingConventionCollection(IEnumerable<INamingConvention> namingConventions)
        {
            foreach (var namingConvention in namingConventions)
            {
                if (_namingConventionMap.ContainsKey(namingConvention.Language))
                {
                    throw new ConfigurationException($"Duplication naming convention for language '{namingConvention.Language}'.");
                }

                _namingConventionMap[namingConvention.Language] = namingConvention;
            }
        }

        public INamingConvention GetNamingConvention(string name, bool throwIfMissing = true)
        {
            if (! _namingConventionMap.TryGetValue(name, out var result) && throwIfMissing)
            {
                throw new ConfigurationException($"Naming convention not found for language '{name}'.");
            }

            return result;
        }
    }
}
