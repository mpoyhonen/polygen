using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Entity.DesignModel
{
    public class EntityAttribute : Core.Impl.DesignModel.DesignModelBase
    {
        public EntityAttribute(Entity entity, IXmlElement element) : base("EntityAttribute", entity.Namespace, element)
        {
            this.Entity = entity;

            this.Name = element.GetAttribute("name")?.Value;
            this.Type = element.GetAttribute("type")?.Value;
        }

        public EntityAttribute(Entity entity, string name, string type) : base("EntityAttribute", entity.Namespace, null)
        {
            this.Entity = entity;

            this.Name = name;
            this.Type = type;
        }

        public Entity Entity { get; }
        public new string Type { get; }
    }
}