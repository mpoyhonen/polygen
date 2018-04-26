using Polygen.Core.DataType;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;
using Polygen.Core.Schema;
using Polygen.Core.Stage;

namespace Polygen.Plugins.Base.Models.Namespace.StageHandler
{
    /// <summary>
    /// Registers Namespace design model.
    /// </summary>
    public class RegisterNamespace: StageHandlerBase
    {
        public RegisterNamespace(): base(StageType.RegisterSchemas, "Namespace-DesignModel")
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IDesignModelGeneratorFactory DesignModelConverterFactory { get; set; }
        public IDataTypeRegistry DataTypeRegistry { get; set; }

        public override void Execute()
        {
            var schema = Schemas.AddSchema(BasePluginConstants.DesignModel_SchemaName, BasePluginConstants.DesignModel_SchemaNamespace);
            var stringType = DataTypeRegistry.Get(BasePluginConstants.DataType_string);

            schema
                .CreateRootElement("DesignModels")
                    .CreateElement("Namespace", "Defines namespace for the design models", c => c.IsMandatory = true)
                        .CreateAttribute("name", stringType, "Namespace name", c => c.IsMandatory = true);

            DesignModelConverterFactory.RegisterFactory(
                schema.RootElement.FindChildElement("Namespace"),
                (xmlElement, context) =>
                {
                    var namespaceName = xmlElement.GetAttribute("name")?.Value;

                    if (string.IsNullOrWhiteSpace(namespaceName))
                    {
                        throw new DesignModelException(context.DesignModel, "Namespace name not set");
                    }

                    var ns = context.Collection.DefineNamespace(namespaceName);

                    context.Namespace = ns;

                    return null;
                }, TODO);
        }
    }
}
