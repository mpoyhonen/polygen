using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Parser
{
    /// <summary>
    /// EntityAttribute design model parser.
    /// </summary>
    public class EntityAttributeParser: DesignModelGeneratorBase
    {
        public EntityAttributeParser() : base(nameof(EntityAttribute), new[] { nameof(Entity) })
        {
        }

        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            var entity = (Entity) context.DesignModel;
            var attribute = new EntityAttribute(entity, xmlElement);
            
            entity.AddAttribute(attribute);
            
            return attribute;
        }
    }
}
