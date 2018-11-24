using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;
using Polygen.Core.Project;
using Polygen.Core.Schema;

namespace Polygen.Core.Impl.Parser
{
    public class XmlElementParser : IXmlElementParser
    {
        public IXmlElement Parse(TextReader reader, ISchema schema, IProjectFile file)
        {
            if (schema.RootElement == null)
            {
                throw new SchemaException($"Schema '{schema.Name}' does not contain a root element.");
            }

            var xmlElement = XElement.Load(reader, LoadOptions.PreserveWhitespace | LoadOptions.SetLineInfo);

            if (xmlElement == null)
            {
                throw new CodeGenerationException($"No root element in file '{file.GetSourcePath(false) ?? file.RelativePath}'");
            }

            var elementLocationInfo = new ParseLocationInfo(xmlElement, file);

            if (xmlElement.Name != schema.RootElement.Name)
            {
                throw new ParseException(elementLocationInfo, $"Invalid root XML element '{xmlElement.Name}'");
            }

            return ParseXmlElement(schema.RootElement, xmlElement, null, file);
        }

        private IXmlElement ParseXmlElement(ISchemaElement schemaElement, XElement sourceXmlElement, IXmlElement parentXmlElement, IProjectFile file)
        {
            var xmlNamespace = sourceXmlElement.Name.Namespace;
            var elementLocationInfo = new ParseLocationInfo(sourceXmlElement, file);
            var xmlElement = new DesignModel.XmlElement(schemaElement, parentXmlElement, elementLocationInfo);

            if (schemaElement.UseContentAsValue)
            {
                xmlElement.Value = sourceXmlElement.Value.Trim();
            }

            foreach (var xmlAttribute in sourceXmlElement.Attributes().Where(x => !x.IsNamespaceDeclaration))
            {
                var attributeLocationInfo = new ParseLocationInfo(xmlAttribute, file);
                var schemaElementAttribute = schemaElement.Attributes.FirstOrDefault(x => CompareName(xmlAttribute, x.Name));

                if (schemaElementAttribute == null)
                {
                    throw new ParseException(attributeLocationInfo, $"Unknown XML attribute '{xmlAttribute.Name}'");
                }

                var attribute = new DesignModel.XmlAttribute(schemaElementAttribute, xmlAttribute.Value, attributeLocationInfo);

                xmlElement.AddAttribute(attribute);
            }

            if (!schemaElement.UseContentAsValue)
            {
                foreach (var childXmlElement in sourceXmlElement.Elements())
                {
                    var childSchemaElement = schemaElement.Children.FirstOrDefault(x => childXmlElement.Name == x.Name);

                    if (childSchemaElement == null)
                    {
                        throw new ParseException(elementLocationInfo, $"Unknown child XML element '{childXmlElement.Name}'");
                    }

                    var childDesignModelElement = ParseXmlElement(childSchemaElement, childXmlElement, xmlElement, file);

                    xmlElement.AddChildElement(childDesignModelElement);
                }
            }

            // Validate that all mandatory attributes and child elements are preset.
            xmlElement.Validate();

            return xmlElement;
        }

        private static bool CompareName(XAttribute attribute, XName name)
        {
            if (attribute.Name.LocalName != name.LocalName)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(attribute.Name.NamespaceName))
            {
                return attribute.Parent.Name.NamespaceName == name.NamespaceName;
            }
            else
            {
                return attribute.Name.NamespaceName == name.NamespaceName;
            }
        }
    }
}
