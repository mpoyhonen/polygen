using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Core.ClassModel
{
    /// <summary>
    /// Interface for a class attribute design model.
    /// </summary>
    public interface IClassAttribute : IDesignModel
    {
        /// <summary>
        /// Attribute data type.
        /// </summary>
        IDataType Type { get; }

        /// <summary>
        /// Attribute value.
        /// </summary>
        object Value { get; set; }
    }

    public class ClassAttributeReference: DesignModelReference<IClassAttribute>
    {
        public ClassAttributeReference(string name, IParseLocationInfo parseLocation) : base(name, null, nameof(IClassAttribute), parseLocation)
        {
        }
    }    
}