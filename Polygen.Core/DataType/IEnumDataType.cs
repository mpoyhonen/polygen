using System.Collections.Generic;
using System.Xml.Linq;

namespace Polygen.Core.DataType
{
    /// <summary>
    /// Interface for a enumeration data type.
    /// </summary>
    public interface IEnumDataType: IDataType
    {
        /// <summary>
        /// Returns 
        /// </summary>
        IEnumerable<EnumDataTypeValue> Values { get; }
    }

    /// <summary>
    /// Contains information about an enumeration value.
    /// </summary>
    public class EnumDataTypeValue
    {
        public EnumDataTypeValue(string name)
        {
            Name = name;
        }
        
        public EnumDataTypeValue(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Enumeration value name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Value description.
        /// </summary>
        public string Description { get; set; }
    }
}
