using System.Collections.Generic;
using System.Linq;
using Polygen.Core.ClassModel;
using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Impl.ClassModel;
using Polygen.Core.Parser;
using Polygen.Plugins.Base.ClassDesignModel;
using Polygen.Plugins.Base.Models.ClassModelMapping;
using Polygen.Plugins.Base.Models.Relation;

namespace Polygen.Plugins.Base.Models.Entity
{
    public class Entity : Core.Impl.ClassModel.ClassModel
    {
        public Entity(string name, INamespace ns, IXmlElement element = null) : base(name, nameof(Entity), ns, element)
        {
        }

        protected override IClassAttribute CreateAttribute(string name, IDataType type, IXmlElement element, IParseLocationInfo parseLocation)
        {
            return new EntityAttribute(name, type, element, parseLocation);
        }
    }
}
