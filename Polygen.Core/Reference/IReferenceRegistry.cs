namespace Polygen.Core.Reference
{
    /// <summary>
    /// Allows registering object references and updates the references once the reference target
    /// objects are registered. This allows automatically resolving the references during parsing.
    /// </summary>
    public interface IReferenceRegistry
    {
        /// <summary>
        /// Adds a reference.
        /// </summary>
        /// <param name="reference"></param>
        /// <typeparam name="T">Type of the reference target object.</typeparam>
        IReference<T> AddReference<T>(IReference<T> reference);

        /// <summary>
        /// Registers a target object. All references pointing to this object are updated.
        /// </summary>
        /// <param name="name">Target object name.</param>
        /// <param name="obj">Target object</param>
        /// <typeparam name="T"></typeparam>
        void RegisterObject<T>(string name, T obj);
    }
}