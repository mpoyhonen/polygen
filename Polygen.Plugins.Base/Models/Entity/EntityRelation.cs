using System.Collections.Generic;
using Polygen.Core.Impl.ClassModel;
using Polygen.Core.Parser;
using Polygen.Plugins.Base.ClassDesignModel;
using Polygen.Plugins.Base.Models.Relation;

namespace Polygen.Plugins.Base.Models.Entity
{
    public class EntityRelation : ClassRelation
    {
        public EntityRelation(string name, IXmlElement element, IParseLocationInfo parseLocation) : base(name, element, parseLocation)
        {
        }
    }
}
