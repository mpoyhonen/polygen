using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polygen.Common.Xml;
using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.OutputModel;
using Polygen.Core.Parser;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base;
using Polygen.Plugins.Base.Output.Xsd;
using Polygen.Plugins.Entity.Output.Backend;

namespace Polygen.Plugins.Entity.StageHandler
{
    /// <summary>
    /// Creates output mopdels from the desing models.
    /// </summary>
    public class RegisterSchemas_DesignModel: StageHandlerBase
    {
        public RegisterSchemas_DesignModel(): base(StageType.RegisterSchemas, "Entity-DesignModel", new[] { "Base-DesingModel" })
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IDesignModelGeneratorFactory DesignModelConverterFactory { get; set; }
        public IDataTypeRegistry DataTypeRegistry { get; set; }

        public override void Execute()
        {
            var schema = this.Schemas.GetSchemaByNamespace(BasePluginConstants.DesignModel_SchemaNamespace);
            var stringType = this.DataTypeRegistry.Get(BasePluginConstants.DataType_string);

            schema
                .RootElement
                .FindChildElement("Namespace")
                .GetBuilder()
                .CreateElement("Entity", "Defines an entity which is the root design model")
                    .CreateAttribute("name", stringType, "Entity name", c => c.IsMandatory = true)
                    .CreateElement("Attribute", "Defines an entity attribute")
                        .CreateAttribute("name", stringType, "Attribute name", c => c.IsMandatory = true)
                        .CreateAttribute("type", stringType, "Attribute type", c => c.IsMandatory = true);

            var entityDesignModelElement = schema.RootElement.FindChildElement("Namespace/Entity");

            this.DesignModelConverterFactory.RegisterFactory(
                entityDesignModelElement,
                (designModelElement, context) => new DesignModel.Entity(context.Namespace, designModelElement)
            );

            this.DesignModelConverterFactory.RegisterFactory(
                entityDesignModelElement.FindChildElement("Attribute"),
                (designModelElement, context) =>
                {
                    var entity = (DesignModel.Entity)context.DesignModel;
                    var attribute = new DesignModel.EntityAttribute(entity, designModelElement);
                    entity.AddAttribute(attribute);
                    return attribute;
                }
            ); 
        }
    }
}
