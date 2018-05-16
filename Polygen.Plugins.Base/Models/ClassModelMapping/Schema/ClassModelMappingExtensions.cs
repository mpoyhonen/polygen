using Polygen.Core.DataType;
using Polygen.Core.Schema;

namespace Polygen.Plugins.Base.Models.ClassModelMapping.Schema
{
    /// <summary>
    /// Helper class for adding Mapping elemet to design model schema.
    /// </summary>
    public static class ClassModelMappingExtensions
    {
        /// <summary>
        /// Creates a schema definition for Mapping element for mapping desing model attributes and relations.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="designModelTypeName"></param>
        /// <param name="addAttributes"></param>
        /// <param name="addRelations"></param>
        /// <param name="dataTypeRegistry"></param>
        /// <returns></returns>
        public static ISchemaElementBuilder AddClassMappingSchema(
            this ISchemaElementBuilder builder,
            string designModelTypeName,
            bool addAttributes,
            bool addRelations,
            IDataTypeRegistry dataTypeRegistry
        )
        {
            // Define the schema elements.
            var stringType = dataTypeRegistry.Get(BasePluginConstants.DataType_string);
            var boolType = dataTypeRegistry.Get(BasePluginConstants.DataType_bool);
            
            builder = builder
                .CreateElement("Mapping", $"Defines a mapping from the source {designModelTypeName}")
                    .CreateAttribute("src", stringType, $"Source {designModelTypeName} name", c => c.IsMandatory = true)
                    .CreateAttribute("name", stringType, $"Mapping name. Needed if multiple mappings to the same design models are defined.")
                    .CreateAttribute("dest", stringType, $"Destination {designModelTypeName} name", c => c.IsMandatory = true)
                    .CreateAttribute("attributes", boolType, "Whether to map attributes")
                    .CreateAttribute("relations", boolType, "Whether to map relations")
                    .CreateAttribute("two-way", boolType, "Whether to to create a mapping for both directions.")
                    .CreateAttribute("add-missing", boolType, "Whether to all attributes and relations which exists at the source and not at the destination.");
            
            if (addAttributes)
            {
                builder
                    .CreateElement("Attribute", "Specifies how a specific attribute will be mapped")
                        .CreateAttribute("src", stringType, "Source design model attribute name", c => c.IsMandatory = true)
                        .CreateAttribute("dest", stringType, "Destination design model attribute name", c => c.IsMandatory = true)
                    .Parent()
                    .CreateElement("SkipAttribute", "Specifies that the specific attribute will not be mapped")
                        .CreateAttribute("name", stringType, "Name of the attribute to skip", c => c.IsMandatory = true);
            }
            
            if (addRelations)
            {
                builder
                    .CreateElement("Relation", "Specifies how a specific attribute will be mapped")
                        .CreateAttribute("src", stringType, "Source design model relation name", c => c.IsMandatory = true)
                        .CreateAttribute("dest", stringType, "Destination design model relation name", c => c.IsMandatory = true)
                        .CreateAttribute("attributes", boolType, "Whether to map attributes")
                        .CreateAttribute("add-missing", boolType, "Whether to all attributes which exists at the source and not at the destination.")
                        .CreateAttribute("two-way", boolType, "Whether to to create a mapping for both directions.")
                    .Parent()
                    .CreateElement("SkipRelation", "Specifies that the specific relation will not be mapped")
                        .CreateAttribute("name", stringType, "Name of the attribute to skip", c => c.IsMandatory = true);
            }
            
            return builder.Parent();
        }
    }
}
