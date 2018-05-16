using Polygen.Core.DataType;
using Polygen.Core.Parser;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.Models.Namespace.Parser;

namespace Polygen.Plugins.Base.Models.Namespace.Schema
{
    /// <summary>
    /// Defines Namespace design model schema.
    /// </summary>
    public class NamespaceSchema: StageHandlerBase
    {
        public NamespaceSchema(): base(StageType.RegisterSchemas, nameof(Namespace))
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IDesignModelGeneratorFactory DesignModelConverterFactory { get; set; }
        public IDataTypeRegistry DataTypeRegistry { get; set; }

        public override void Execute()
        {
            // Define the schema elements.
            var schema = Schemas.AddSchema(BasePluginConstants.DesignModel_SchemaName, BasePluginConstants.DesignModel_SchemaNamespace);
            var stringType = DataTypeRegistry.Get(BasePluginConstants.DataType_string);

            schema
                .CreateRootElement("DesignModels")
                    .CreateElement("Namespace", "Defines namespace for the design models", c => c.IsMandatory = true)
                        .CreateAttribute("name", stringType, "Namespace name", c => c.IsMandatory = true);

            // Register parser.
            DesignModelConverterFactory.RegisterFactory(schema.RootElement.FindChildElement("Namespace"), new NamespaceParser());
        }
    }
}
