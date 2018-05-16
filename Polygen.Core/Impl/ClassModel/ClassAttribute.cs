using Polygen.Core.ClassModel;
using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Impl.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Core.Impl.ClassModel
{
    public class ClassAttribute: DesignModelBase, IClassAttribute
    {
        public ClassAttribute(string name, IDataType type, IXmlElement element = null, IParseLocationInfo parseLocation = null) 
            : base(name, nameof(ClassAttribute), null, element, parseLocation)
        {
            Type = type;
        }

        public IDataType Type { get; }
        public object Value { get; set;  }
    }
}