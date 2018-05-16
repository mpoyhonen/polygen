using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Parser
{
    /// <summary>
    /// EntityRelationAttribute design model parser.
    /// </summary>
    public class EntityRelationAttributeParser: DesignModelGeneratorBase
    {
        private readonly IDataTypeRegistry _dataTypeRegistry;

        public EntityRelationAttributeParser(IDataTypeRegistry dataTypeRegistry)
        {
            _dataTypeRegistry = dataTypeRegistry;
        }
        
        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            var relation = (EntityRelation)context.DesignModel;
            var name = xmlElement.GetStringAttributeValue("name");
            var type = _dataTypeRegistry.Get(xmlElement.GetStringAttributeValue("type"));
            var attribute = new EntityAttribute(name, type, xmlElement, xmlElement.ParseLocation);
            
            relation.AddAttribute(attribute);
            
            // Don't register this design model globally.
            return null;
        }
    }
}
