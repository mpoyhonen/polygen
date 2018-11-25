using System;
using System.Collections.Generic;
using System.Text;

namespace Polygen.Core.NamingConvention
{
    /// <summary>
    /// Interface for keeping track of all naming conventions.
    /// </summary>
    public interface INamingConventionCollection
    {
        /// <summary>
        /// Tries to get a naming convention by name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="throwIfMissing"></param>
        /// <returns></returns>
        INamingConvention GetNamingConvention(string name, bool throwIfMissing = true);
    }
}
