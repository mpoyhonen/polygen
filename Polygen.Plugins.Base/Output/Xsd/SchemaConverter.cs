using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Polygen.Core;
using Polygen.Core.DataType;
using Polygen.Core.Schema;

namespace Polygen.Plugins.Base.Output.Xsd
{
    /// <summary>
    /// Converts schema elements into XSD output mocel.
    /// </summary>
    public class SchemaConverter
    {
        private static readonly XNamespace XsdNamespace = "http://www.w3.org/2001/XMLSchema";

        private Dictionary<string, IDataType> _usedTypes = new Dictionary<string, IDataType>();

        public SchemaXsdOutputModel Convert(ISchema schema)
        {
            var rootXmlElement = new XElement(XsdNamespace + "schema",
                new XAttribute(XNamespace.Xmlns + "xs", XsdNamespace.NamespaceName),
                new XAttribute(XNamespace.Xmlns + "tns", schema.Namespace),
                new XAttribute("targetNamespace", schema.Namespace),
                new XAttribute("elementFormDefault", "qualified")
            );

            rootXmlElement.Add(Convert(schema.RootElement));

            // Allow data types to do post processing.
            foreach (var dataType in this._usedTypes.Values)
            {
                dataType.PostProcessXsdDefinition(rootXmlElement);
            }

            return new SchemaXsdOutputModel(rootXmlElement);
        }

        private XElement Convert(ISchemaElement schemaElement)
        {
            // Create the XML element.
            var xmlElement = new XElement(XsdNamespace + "element",
                new XAttribute("name", schemaElement.Name.LocalName)
            );

            // For non-root elements add minOccurs and maxOccurs.
            if (schemaElement != schemaElement.Schema.RootElement)
            {
                xmlElement.Add(
                    new XAttribute("minOccurs", schemaElement.IsMandatory ? "1" : "0"),
                    new XAttribute("maxOccurs", schemaElement.AllowMultiple ? "unbounded" : "1")
                );
            }

            // Add the documentation element.
            this.AddDocumentationElement(xmlElement, schemaElement.Description);

            // Create complexType element for attributes and child elements.
            var complexTypeXmlElement = new XElement(XsdNamespace + "complexType");
            var attributeDestinationXmlElement = complexTypeXmlElement;

            xmlElement.Add(complexTypeXmlElement);

            // Add children or child value.
            if (schemaElement.Children.Count > 0)
            {
                var allXmlElement = new XElement(XsdNamespace + "sequence");

                complexTypeXmlElement.Add(allXmlElement);

                foreach (var childSchemaElement in schemaElement.Children)
                {
                    var childXmlElement = this.Convert(childSchemaElement);

                    allXmlElement.Add(childXmlElement);
                }
            }
            else if (schemaElement.UseContentAsValue)
            {
                if (schemaElement.ValueType == null)
                {
                    throw new Exception($"Data type must be set for schema element '{schemaElement.Name}");
                }

                var simpleContentXmlElement = new XElement(XsdNamespace + "simpleContent");
                var extensionXmlElement = new XElement(XsdNamespace + "extension", 
                    new XAttribute("base", schemaElement.ValueType.XsdName)
                );

                simpleContentXmlElement.Add(extensionXmlElement);
                complexTypeXmlElement.Add(simpleContentXmlElement);
                attributeDestinationXmlElement = extensionXmlElement;
                this.RegisterTypeDefinition(schemaElement.ValueType);
            }

            // Add all attributes.
            foreach (var schemaElementAttribute in schemaElement.Attributes)
            {
                if (schemaElementAttribute.Type == null)
                {
                    throw new Exception($"Data type must be set for schema element attribute '{schemaElementAttribute.Name}");
                }

                var attributeXmlElement = this.CreateAttributeElement(schemaElementAttribute);

                attributeDestinationXmlElement.Add(attributeXmlElement);
                this.RegisterTypeDefinition(schemaElementAttribute.Type);
            }

            // Remove the complexType element, if it is not needed after all.
            if (complexTypeXmlElement.Elements().FirstOrDefault() == null)
            {
                complexTypeXmlElement.Remove();
            }

            return xmlElement;
        }

        /// <summary>
        /// Registers the used data type definition for post-processing.
        /// </summary>
        /// <param name="element"></param>
        private void RegisterTypeDefinition(IDataType dataType)
        {
            if (!this._usedTypes.ContainsKey(dataType.Name))
            {
                this._usedTypes[dataType.Name] = dataType;
            }
        }

        private XElement CreateAttributeElement(ISchemaElementAttribute schemaElementAttribute)
        {
            var attributeXmlElement = new XElement(XsdNamespace + "attribute",
                new XAttribute("name", schemaElementAttribute.Name.LocalName),
                new XAttribute("type", schemaElementAttribute.Type.XsdName)
            );

            if (schemaElementAttribute.IsMandatory)
            {
                attributeXmlElement.Add(new XAttribute("use", "required"));
            }

            this.AddDocumentationElement(attributeXmlElement, schemaElementAttribute.Description);

            return attributeXmlElement;
        }

        private void AddDocumentationElement(XElement xmlElement, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return;
            }

            xmlElement.Add(
                new XElement(XsdNamespace + "annotation",
                    new XElement(XsdNamespace + "documentation", description)
                )
            );
        }
    }
}
