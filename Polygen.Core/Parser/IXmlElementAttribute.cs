using Polygen.Core.Parser;
using Polygen.Core.Schema;

namespace Polygen.Core.Parser
{
    /// <summary>
    /// Interface for an element attribute parsed from XML using a schema element attribute.
    /// </summary>
    public interface IXmlElementAttribute
    {
        /// <summary>
        /// Schema element attribute this attribute was parsed from.
        /// </summary>
        ISchemaElementAttribute Definition { get; }
        /// <summary>
        /// Contains parse location information for error messages.
        /// </summary>
        IParseLocationInfo ParseLocation { get; }
        /// <summary>
        /// Attribute value.
        /// </summary>
        string Value { get; set; }
    }
}
