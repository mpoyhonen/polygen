using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using Polygen.Core.DataType;
using System.Collections.Generic;
using System;

namespace Polygen.Core.Impl.DataType
{
    public class DataTypeRegistry : IDataTypeRegistry
    {
        private Dictionary<string, IDataType> _dataTypes = new Dictionary<string, IDataType>();

        public IDataType Get(string name, bool throwIfMissing)
        {
            if (_dataTypes.TryGetValue(name, out var res))
            {
                return res;
            }

            throw new Exception($"Data type '{name}' is not registered.");
        }

        public void Add(IDataType dataType)
        {
            if (_dataTypes.TryGetValue(dataType.Name, out var res))
            {
                throw new Exception($"Data type '{dataType.Name}' is already registered.");
            }

            _dataTypes.Add(dataType.Name, dataType);
        }
    }
}
