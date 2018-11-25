using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using Polygen.Core.DataType;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Polygen.Core.Impl.DataType
{
    public class DataTypeRegistry : IDataTypeRegistry
    {
        private readonly Dictionary<string, IDataType> _dataTypes = new Dictionary<string, IDataType>();
        private IEnumDataType _availableTypesEnumType;

        public IDataType Get(string name, bool throwIfMissing)
        {
            if (_dataTypes.TryGetValue(name, out var res))
            {
                return res;
            }

            throw new Exception($"Data type '{name}' is not registered.");
        }

        public IEnumDataType GetAvailableTypesEnumType()
        {
            if (_availableTypesEnumType == null)
            {
                var values = _dataTypes.Values.Select(dataType => new EnumDataTypeValue(dataType.Name, dataType.Description));
                
                _availableTypesEnumType = new EnumType("AvailableTypes", "Defines all registered types", values.ToArray());
            }

            return _availableTypesEnumType;
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
