using System.Collections.Generic;
using System.Xml.Linq;
using Polygen.Core.DataType;

namespace Polygen.Core.Schema
{
    /// <summary>
    /// Interface for a schema element which maps to an XML element.
    /// </summary>
    public interface ISchemaElement
    {
        /// <summary>
        /// Owner schema.
        /// </summary>
        ISchema Schema { get; }
        /// <summary>
        /// Parent element.
        /// </summary>
        ISchemaElement Parent { get; }
        /// <summary>
        /// Defines XML element name and namespace.
        /// </summary>
        XName Name { get; }
        /// <summary>
        /// Documentation for this schema element.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Defines the attributes for this element.
        /// </summary>
        IReadOnlyList<ISchemaElementAttribute> Attributes { get; }
        /// <summary>
        /// Defines the possible child elements for this element.
        /// </summary>
        IReadOnlyList<ISchemaElement> Children { get; }

        /// <summary>
        /// Whether this is an optional or mandatory element.
        /// </summary>
        bool IsMandatory { get; set; }
        /// <summary>
        /// Whether this element can appear more than once under the parent.
        /// </summary>
        bool AllowMultiple { get; set; }
        /// <summary>
        /// Whether to use the text content of th XML element as the value of this element.
        /// </summary>
        bool UseContentAsValue { get; set; }
        /// <summary>
        /// Content value data type.
        /// </summary>
        IDataType ValueType { get; set; }

        /// <summary>
        /// Adds an attribute.
        /// </summary>
        /// <param name="attribute"></param>
        void AddAttribute(ISchemaElementAttribute attribute);
        /// <summary>
        /// Removes an attribute.
        /// </summary>
        /// <param name="name"></param>
        void RemoveAttribute(string name);
        /// <summary>
        /// Adds a child schema element.
        /// </summary>
        /// <param name="schemaElement"></param>
        void AddChildElement(ISchemaElement schemaElement);
        /// <summary>
        /// Removes a child schema element.
        /// </summary>
        /// <param name="name"></param>
        void RemoveChildElement(string name);
        /// <summary>
        /// Returns a new builder for creating new child elements and attributes.
        /// </summary>
        /// <returns></returns>
        ISchemaElementBuilder GetBuilder();
        /// <summary>
        /// Returns the first matching child element (path can contain subpaths separated by '/').
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        ISchemaElement FindChildElement(string path);
    }
}