using System.Xml.Linq;
using Polygen.Core.DataType;
using Polygen.Core.Schema;
using JetBrains.Annotations;
using Polygen.Core.Impl.Parser;
using Polygen.Core.Parser;

namespace Polygen.Core.DesignModel
{
    /// <inheritdoc />
    public class DesignModelProperty: IDesignModelProperty
    {
        public DesignModelProperty(string name, IDataType type, object value, ISchemaElementAttribute definition = null, IParseLocationInfo parseLocation = null)
        {
            Name = name;
            Type = type;
            Value = value;
            ParseLocation = parseLocation;
            Definition = definition;
        }

        public string Name { get; }
        public IDataType Type { get; }
        public object Value { get; set; }
        public IParseLocationInfo ParseLocation { get; }
        [CanBeNull]
        public ISchemaElementAttribute Definition { get;  }

        public string StringValue => (string) Value;
        public bool BoolValue => (bool) Value;
        public int IntValue => (int) Value;
    }
}