using Polygen.Core.Parser;
using Polygen.Plugins.Base.ModelContract;

namespace Polygen.Plugins.Base.Models.Entity
{
    public class EntityAttribute : Core.Impl.DesignModel.DesignModelBase, IAttribute
    {
        public EntityAttribute(Entity entity, IXmlElement element) : base("EntityAttribute", entity.Namespace, element)
        {
            this.Model = entity;

            this.Name = element.GetAttribute("name")?.Value;
            this.AttributeType = element.GetAttribute("type")?.Value;
        }

        public EntityAttribute(Entity entity, string name, string type) : base("EntityAttribute", entity.Namespace, null)
        {
            this.Model = entity;

            this.Name = name;
            this.AttributeType = type;
        }

        public EntityAttribute(Entity entity, IAttribute source) : base("EntityAttribute", entity.Namespace, null)
        {
            this.Model = entity;

            this.Name = source.Name;
            this.AttributeType = source.Type;
            this.Value = source.Value;
        }

        public IModel Model { get; }
        public string AttributeType { get; }
        public string Value { get; }
    }
}