using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.Models.ClassModelDerivation.Parser;
using Polygen.Plugins.Base.Models.ClassModelDerivation.Schema;
using Polygen.Plugins.Base.Models.ClassModelMapping.Parser;
using Polygen.Plugins.Base.Models.ClassModelMapping.Schema;
using Polygen.Plugins.Base.Models.Entity.Parser;
using Polygen.Plugins.Base.Models.Relation.Schema;

namespace Polygen.Plugins.Base.Models.Entity.Schema
{
    /// <summary>
    /// Defines an entity model mapping element schema.
    /// </summary>
    public class EntityModelDerivationSchema: StageHandlerBase
    {
        public EntityModelDerivationSchema(): base(StageType.RegisterSchemas, nameof(EntityModelDerivationSchema), nameof(EntityRelationAttributeSchema))
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

            schema
                .RootElement
                .FindChildElement("Namespace/Entity")
                .GetBuilder()
                .AddDeriveFromSchema("entity", true, true, DataTypeRegistry);

            // Register parsers.
            var deriveFromElement = schema.RootElement.FindChildElement("Namespace/Entity/DeriveFrom");
            var parser = new ClassDerivationParser(DesignModelCollection);
            
            DesignModelConverterFactory.RegisterFactory(deriveFromElement, parser);
        }
    }
}
