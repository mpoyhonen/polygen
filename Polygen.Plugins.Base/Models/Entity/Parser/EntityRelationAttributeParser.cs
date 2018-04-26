using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Parser
{
    /// <summary>
    /// EntityRelationAttribute design model parser.
    /// </summary>
    public class EntityRelationAttributeParser: DesignModelGeneratorBase
    {
        public EntityRelationAttributeParser() 
            : base(nameof(EntityRelationAttribute), new[] { nameof(EntityRelation) })
        {
        }

        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            var relation = (EntityRelation)context.DesignModel;
            var attribute = new EntityRelationAttribute(relation, xmlElement);
            
            relation.AddAttribute(attribute);
            
            return attribute;
        }
    }
}
