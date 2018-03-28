using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using System.Collections.Generic;

namespace Polygen.Plugins.Entity.DesignModel
{
    public class Entity : Core.Impl.DesignModel.DesignModelBase
    {
        private readonly List<EntityAttribute> _attributes = new List<EntityAttribute>();

        public Entity(INamespace ns, IXmlElement element = null) : base("Entity", ns, element)
        {
            this.Name = element?.GetAttribute("name")?.Value;
        }

        public IEnumerable<EntityAttribute> Attributes => this._attributes;

        public void AddAttribute(EntityAttribute entityAttribute)
        {
            this._attributes.Add(entityAttribute);
        }
    }
}
