using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Polygen.Core.Schema;

namespace Polygen.Core.DataType
{
    /// <summary>
    /// Interface for data type definitions.
    /// </summary>
    public interface IDataTypeRegistry
    {
        /// <summary>
        /// Adds a new data type to the registry.
        /// </summary>
        void Add(IDataType dataType);
        /// <summary>
        /// Tries to find a data type from the registry.
        /// </summary>
        IDataType Get(string name, bool throwIfMissing = true);
    }
}
