using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.Models.ClassModelMapping.Parser;
using Polygen.Plugins.Base.Models.ClassModelMapping.Schema;
using Polygen.Plugins.Base.Models.Entity.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Schema
{
    /// <summary>
    /// Defines an entity model mapping element schema.
    /// </summary>
    public class EntityModelMappingSchema: StageHandlerBase
    {
        public EntityModelMappingSchema(): base(StageType.RegisterSchemas, nameof(EntityModelMappingSchema), nameof(EntityModelDerivationSchema))
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
                .AddClassMappingSchema("entity", true, true, DataTypeRegistry);

            // Register parsers.
            var modelMappingElement = schema.RootElement.FindChildElement("Namespace/Entity/Mapping");
            var parser = new ClassMappingParser(DesignModelCollection);
            
            DesignModelConverterFactory.RegisterFactory(modelMappingElement, parser);
        }
    }
}
