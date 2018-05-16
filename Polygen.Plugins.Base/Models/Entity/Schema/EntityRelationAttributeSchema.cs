using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.Models.Entity;
using Polygen.Plugins.Base.Models.Entity.Parser;
using Polygen.Plugins.Base.Models.Entity.Schema;

namespace Polygen.Plugins.Base.Models.Relation.Schema
{
    /// <summary>
    /// Defines an entity relation attribute design model schema.
    /// </summary>
    public class EntityRelationAttributeSchema: StageHandlerBase
    {
        public EntityRelationAttributeSchema(): base(StageType.RegisterSchemas, nameof(EntityRelationAttributeSchema), nameof(EntityRelationSchema))
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

            schema
                .RootElement
                .FindChildElement("Namespace/Entity/Relation")
                .GetBuilder()
                .CreateElement("Attribute", "Defines an entity attribute", c => c.AllowMultiple = true)
                    .CreateAttribute("name", stringType, "Attribute name", c => c.IsMandatory = true)
                    .CreateAttribute("type", stringType, "Attribute type", c => c.IsMandatory = true);

            var designModelElement = schema.RootElement.FindChildElement("Namespace/Entity/Relation/Attribute");

            // Register parser.
            DesignModelConverterFactory.RegisterFactory(designModelElement, new EntityRelationAttributeParser(DataTypeRegistry));
        }
    }
}
