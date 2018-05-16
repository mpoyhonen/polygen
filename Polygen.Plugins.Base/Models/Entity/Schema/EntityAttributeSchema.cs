using Polygen.Core.DataType;
using Polygen.Core.Parser;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.Models.Entity.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Schema
{
    /// <summary>
    /// Defines EntityAttribute design model schema.
    /// </summary>
    public class EntityAttributeSchema: StageHandlerBase
    {
        public EntityAttributeSchema(): base(StageType.RegisterSchemas, nameof(EntityAttributeSchema), nameof(EntitySchema))
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IDesignModelGeneratorFactory DesignModelConverterFactory { get; set; }
        public IDataTypeRegistry DataTypeRegistry { get; set; }

        public override void Execute()
        {
            // Define the schema elements.
            var schema = Schemas.GetSchemaByNamespace(BasePluginConstants.DesignModel_SchemaNamespace);
            var stringType = DataTypeRegistry.Get(BasePluginConstants.DataType_string);

            var entityDesignModelElement = schema.RootElement.FindChildElement("Namespace/Entity");

            entityDesignModelElement.GetBuilder()
                .CreateAttribute("name", stringType, "Entity name", c => c.IsMandatory = true)
                .CreateElement("Attribute", "Defines an entity attribute")
                .CreateAttribute("name", stringType, "Attribute name", c => c.IsMandatory = true)
                .CreateAttribute("type", stringType, "Attribute type", c => c.IsMandatory = true);

            var designModelElement = entityDesignModelElement.FindChildElement("Attribute");

            // Register parser.
            DesignModelConverterFactory.RegisterFactory(designModelElement, new EntityAttributeParser(DataTypeRegistry));
        }
    }
}
