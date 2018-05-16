using Polygen.Core.DataType;
using Polygen.Core.Parser;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.Models.Entity.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Schema
{
    /// <summary>
    /// Defines Entity design model schema.
    /// </summary>
    public class EntitySchema: StageHandlerBase
    {
        public EntitySchema(): base(StageType.RegisterSchemas, nameof(EntitySchema))
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IDesignModelGeneratorFactory DesignModelConverterFactory { get; set; }

        public override void Execute()
        {
            // Define the schema elements.
            var schema = Schemas.GetSchemaByNamespace(BasePluginConstants.DesignModel_SchemaNamespace);

            schema
                .RootElement
                .FindChildElement("Namespace")
                .GetBuilder()
                .CreateElement("Entity", "Defines an entity which is the root design model");

            // Register parser.
            var designModelElement = schema.RootElement.FindChildElement("Namespace/Entity");

            DesignModelConverterFactory.RegisterFactory(designModelElement, new EntityParser());
        }
    }
}
