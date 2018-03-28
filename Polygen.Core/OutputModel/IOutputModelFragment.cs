namespace Polygen.Core.OutputModel
{
    /// <summary>
    /// Interface for a fragment inside an output model.
    /// </summary>
    public interface IOutputModelFragment
    {
        /// <summary>
        /// Defines the fragment type. 
        /// </summary>
        string Type { get; }
    }
}