using Polygen.Core.ClassModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.ClassModelDerivation
{
    /// <summary>
    /// Defines a mapping from one class-like desing model attribute to another.
    /// </summary>
    public class AttributeDerivation
    {
        /// <summary>
        /// Source design model attribute reference.
        /// </summary>
        public ClassAttributeReference Source { get; set; }
        
        /// <summary>
        /// Destination design model attribute reference,
        /// </summary>
        public ClassAttributeReference Destination { get; set; }
        
        /// <summary>
        /// Destination design model type. If not set, the source attribute type is used.
        /// </summary>
        public string DestinationType { get; set; }
        
        /// <summary>
        /// Derivation element parse location for error messages.
        /// </summary>
        public IParseLocationInfo ParseLocation { get; set; }
    }
}