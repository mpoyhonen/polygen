using System.Xml.Linq;

namespace Polygen.Core.DataType
{
    /// <summary>
    /// Interface value data type.
    /// </summary>
    public interface IDataType
    {
        /// <summary>
        /// Data type name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Data type name used in XSD when referring to this type.
        /// </summary>
        string XsdName { get; }
        /// <summary>
        /// Description of this data types used for documentation.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Called for each data type which is used in the schema, so the type implementations
        /// can add additional data in the generated XSD.
        /// </summary>
        /// <returns></returns>
        void PostProcessXsdDefinition(XElement schemaRootElement);
    }
}
