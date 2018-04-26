using System.Collections.Generic;
using Polygen.Core.Parser;
using Polygen.Plugins.Base.ModelContract;
using Polygen.Plugins.Base.Models.Relation;

namespace Polygen.Plugins.Base.Models.Entity
{
    public class EntityRelation : Core.Impl.DesignModel.DesignModelBase, IModelRelation
    {
        private readonly List<EntityRelationAttribute> _attributes = new List<EntityRelationAttribute>();

        public EntityRelation(Entity source, Entity destination, IXmlElement element = null) : base("EntityRelation", null, element)
        {
            this.Name = element?.GetAttribute("name")?.Value ?? destination.Name;
            this.Source = source;
            this.Destination = destination;
        }

        public IModel Source { get; }
        public IModel Destination { get; }
        public IEnumerable<IAttribute> Attributes => this._attributes;
        
        public void CopyFrom(IModelRelation source)
        {
            Name = source.Name;
            
            foreach (var attribute in source.Attributes)
            {
                AddAttribute(new EntityRelationAttribute(this, attribute));
            }
        }

        public void AddAttribute(EntityRelationAttribute entityRelationAttribute)
        {
            this._attributes.Add(entityRelationAttribute);
        }
    }
}
