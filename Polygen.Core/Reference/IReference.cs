using Polygen.Core.Parser;

namespace Polygen.Core.Reference
{
    /// <summary>
    /// Contains a reference to another object. Used to keep track of the name of the object when the
    /// direct object reference is not yet available. 
    /// </summary>
    public interface IReference<T>
    {
        /// <summary>
        /// Target object name. Must be unique for the target object type.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Target object type. Needed when there can be more than one design model with the same name. 
        /// </summary>
        string Type { get; set; }
        /// <summary>
        /// Resolved target object.
        /// </summary>
        T Target { get; set; }
        /// <summary>
        /// Location from where this reference was parsed from. Used for error messages.
        /// </summary>
        IParseLocationInfo ParseLocation { get; set; }
    }
}