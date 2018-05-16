using Polygen.Core.DataType;
using Polygen.Core.Schema;
using JetBrains.Annotations;
using Polygen.Core.Parser;

namespace Polygen.Core.DesignModel
{
    /// <summary>
    /// Interface for a property in the design model (e.g. name). These are implemented
    /// as a generic interface to allow easily to copy definitions from one design model
    /// to another. 
    /// </summary>
    public interface IDesignModelProperty
    {
        /// <summary>
        /// Property name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Property type.
        /// </summary>
        IDataType Type { get; }
        /// <summary>
        /// Provides access to the value.
        /// </summary>
        object Value { get; set; }
        /// <summary>
        /// Localtion where this property was parsed from.
        /// </summary>
        IParseLocationInfo ParseLocation { get; }        
        /// <summary>
        /// Optional definition in the design model schema.
        /// </summary>
        [CanBeNull]
        ISchemaElementAttribute Definition { get;  }
        /// <summary>
        /// Returns value as a string. Throws an exception if the value is not a string.
        /// </summary>
        string StringValue { get;  }
        /// <summary>
        /// Returns value as a boolean. Throws an exception if the value is not a boolean.
        /// </summary>
        bool BoolValue { get;  }
        /// <summary>
        /// Returns value as an integer. Throws an exception if the value is not an integer.
        /// </summary>
        int IntValue { get;  }
    }
}