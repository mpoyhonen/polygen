using System.Xml.Linq;
using Polygen.Core.DataType;

namespace Polygen.Core.Schema
{
    /// <summary>
    /// Defines an attribute for a schema element. This maps to an XML attribute in the schema element XML element.
    /// </summary>
    public interface ISchemaElementAttribute
    {
        /// <summary>
        /// Owning schema element.
        /// </summary>
        ISchemaElement Element { get; }
        /// <summary>
        /// Defines XML attribute name and namespace.
        /// </summary>
        XName Name { get; }
        /// <summary>
        /// Attribute type.
        /// </summary>
        IDataType Type { get; set; }
        /// <summary>
        /// Documentation for this schema element attribute.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Whether this attribute is mandatory.
        /// </summary>
        bool IsMandatory { get; set; }
        /// <summary>
        /// Default value when the attribute is optional.
        /// </summary>
        string DefaultValue { get; set; }
    }
}
