using Polygen.Core.ClassModel;
using Polygen.Core.DataType;
using Polygen.Core.Impl.ClassModel;
using Polygen.Core.Parser;
using Polygen.Plugins.Base.ClassDesignModel;

namespace Polygen.Plugins.Base.Models.Entity
{
    public class EntityAttribute : ClassAttribute
    {
        public EntityAttribute(string name, IDataType type, IXmlElement element, IParseLocationInfo parseLocation) : base(name, type, element, parseLocation)
        {
        }
    }
}