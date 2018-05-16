using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Parser
{
    /// <summary>
    /// EntityAttribute design model parser.
    /// </summary>
    public class EntityAttributeParser: DesignModelGeneratorBase
    {
        private readonly IDataTypeRegistry _dataTypeRegistry;

        public EntityAttributeParser(IDataTypeRegistry dataTypeRegistry)
        {
            _dataTypeRegistry = dataTypeRegistry;
        }

        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            var entity = (Entity) context.DesignModel;
            var name = xmlElement.GetStringAttributeValue("name");
            var type = _dataTypeRegistry.Get(xmlElement.GetStringAttributeValue("type"));
            var attribute = new EntityAttribute(name, type, xmlElement, xmlElement.ParseLocation);
            
            entity.AddAttribute(attribute);
            
            // Don't register this design model globally.
            return null;
        }
    }
}
