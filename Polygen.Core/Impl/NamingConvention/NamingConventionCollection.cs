using System;
using System.Collections.Generic;
using System.Text;
using Polygen.Core.Exceptions;
using Polygen.Core.NamingConvention;

namespace Polygen.Core.Impl.NamingConvention
{
    public class NamingConventionCollection : INamingConventionCollection
    {
        private Dictionary<string, IClassNamingConvention> _classNamingConventionMap = new Dictionary<string, IClassNamingConvention>();
        
        public NamingConventionCollection(IEnumerable<IClassNamingConvention> classNamingConventions)
        {
            foreach (var classNamingConvention in classNamingConventions)
            {
                if (_classNamingConventionMap.ContainsKey(classNamingConvention.Language))
                {
                    throw new ConfigurationException($"Duplication class naming convention for language '{classNamingConvention.Language}'.");
                }

                _classNamingConventionMap[classNamingConvention.Language] = classNamingConvention;
            }
        }

        public IClassNamingConvention GetClassNamingConvention(string name, bool throwIfMissing = true)
        {
            if (! _classNamingConventionMap.TryGetValue(name, out var result) && throwIfMissing)
            {
                throw new ConfigurationException($"Class naming convention not found for language '{name}'.");
            }

            return result;
        }
    }
}
