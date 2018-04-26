using Polygen.Core.Parser;
using Polygen.Plugins.Base.ModelContract;

namespace Polygen.Plugins.Base.Models.Entity
{
    public class EntityRelationAttribute : Core.Impl.DesignModel.DesignModelBase, IAttribute
    {
        public EntityRelationAttribute(EntityRelation relation, IXmlElement element) : base("EntityRelationAttribute", null, element)
        {
            Relation = relation;

            Name = element.GetAttribute("name")?.Value;
            Type = element.GetAttribute("type")?.Value;
        }
        
        public EntityRelationAttribute(EntityRelation relation, IAttribute source) : base("EntityRelationAttribute", null, null)
        {
            this.Relation = relation;

            this.Name = source.Name;
            this.AttributeType = source.Type;
            this.Value = source.Value;
        }

        public EntityRelation Relation { get; }
        public string AttributeType { get; }
        public string Value { get; }
    }
}