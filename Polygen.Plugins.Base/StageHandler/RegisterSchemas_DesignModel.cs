using Polygen.Core.DataType;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;
using Polygen.Core.Schema;
using Polygen.Core.Stage;

namespace Polygen.Plugins.Base.StageHandler
{
    /// <summary>
    /// Creates output mopdels from the desing models.
    /// </summary>
    public class RegisterSchemas_DesignModel: StageHandlerBase
    {
        public RegisterSchemas_DesignModel(): base(StageType.RegisterSchemas, "Base-DesignModel")
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IDesignModelGeneratorFactory DesignModelConverterFactory { get; set; }
        public IDataTypeRegistry DataTypeRegistry { get; set; }

        public override void Execute()
        {
            var schema = this.Schemas.AddSchema(BasePluginConstants.DesignModel_SchemaName, BasePluginConstants.DesignModel_SchemaNamespace);
            var stringType = this.DataTypeRegistry.Get(BasePluginConstants.DataType_string);

            schema
                .CreateRootElement("DesignModels")
                    .CreateElement("Namespace", "Defines namespace for the design models", c => c.IsMandatory = true)
                        .CreateAttribute("name", stringType, "Namespace name", c => c.IsMandatory = true);

            this.DesignModelConverterFactory.RegisterFactory(
                schema.RootElement.FindChildElement("Namespace"),
                (designModelElement, context) =>
                {
                    var namespaceName = context.XmlElement.GetAttribute("name")?.Value;

                    if (string.IsNullOrWhiteSpace(namespaceName))
                    {
                        throw new DesignModelException(context.DesignModel, "Namespace name not set");
                    }

                    var ns = context.Collection.DefineNamespace(namespaceName);

                    context.Namespace = ns;

                    return null;
                }
            );
        }
    }
}
