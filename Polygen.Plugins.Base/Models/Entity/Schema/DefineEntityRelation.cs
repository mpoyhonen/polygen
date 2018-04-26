using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.Models.Entity.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Schema
{
    /// <summary>
    /// Defines an entity relation design model schema.
    /// </summary>
    public class DefineEntityRelation: StageHandlerBase
    {
        public DefineEntityRelation(): base(StageType.RegisterSchemas, nameof(EntityRelation))
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IDesignModelGeneratorFactory DesignModelConverterFactory { get; set; }
        public IDataTypeRegistry DataTypeRegistry { get; set; }
        public IDesignModelCollection DesignModelCollection { get; set; }

        public override void Execute()
        {
            // Define the schema elements.
            var schema = Schemas.GetSchemaByNamespace(BasePluginConstants.DesignModel_SchemaNamespace);
            var stringType = DataTypeRegistry.Get(BasePluginConstants.DataType_string);
            var boolType = DataTypeRegistry.Get(BasePluginConstants.DataType_bool);

            schema
                .RootElement
                .FindChildElement("Namespace/Entity")
                .GetBuilder()
                .CreateElement("Relation", "Defines a relation to another entity")
                    .CreateAttribute("name", stringType, "Relation name")
                    .CreateAttribute("destination", stringType, "Destination entity name", c => c.IsMandatory = true)
                    .CreateAttribute("mandatory", boolType, "Whether this is a mandatory relation", c => c.IsMandatory = true);

            var designModelElement = schema.RootElement.FindChildElement("Namespace/Entity/Relation");

            // Register parser.
            DesignModelConverterFactory.RegisterFactory(designModelElement, new EntityRelationParser(DesignModelCollection));
        }
    }
}
