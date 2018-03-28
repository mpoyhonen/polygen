using System.Xml.Linq;
using Polygen.Core.Schema;
using Polygen.Core.DataType;

namespace Polygen.Core.Impl.Schema
{
    /// <summary>
    /// Defines an attribute for a schema element. This maps to an XML attribute in the schema element XML element.
    /// </summary>
    public class SchemaElementAttribute : ISchemaElementAttribute
    {
        public SchemaElementAttribute(ISchemaElement element, XName name, IDataType type, string description)
        {
            if (string.IsNullOrWhiteSpace(name.NamespaceName))
            {
                name = element.Schema.Namespace + name.LocalName;
            }

            this.Element = element;
            this.Name = name;
            this.Type = type;
            this.Description = description;
        }

        /// <summary>
        /// Owning schema element.
        /// </summary>
        public ISchemaElement Element { get; }
        /// <summary>
        /// Defines XML attribute name and namespace.
        /// </summary>
        public XName Name { get; }
        /// <summary>
        /// Attribute type.
        /// </summary>
        public IDataType Type { get; set; }
        /// <summary>
        /// Documentation for this schema element.
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// Whether this attribute is mandatory.
        /// </summary>
        public bool IsMandatory { get; set; }
        /// <summary>
        /// Default value when the attribute is optional.
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
