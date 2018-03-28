using Polygen.Core.OutputConfiguration;
using Polygen.Core.OutputModel;
using Polygen.Core.Parser;
using System.Collections.Generic;

namespace Polygen.Core.DesignModel
{
    /// <summary>
    /// Interface for a design model parsed for a design model element data.
    /// </summary>
    public interface IDesignModel
    {
        /// <summary>
        /// Determines the design model type.
        /// </summary>
        string Type { get; }
        /// <summary>
        /// Design model name. Can be null if the model type isn't named.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Design model name prefixed with the namespace name. Can be null if the model type isn't named.
        /// </summary>
        string FullyQualifiedName { get; }
        /// <summary>
        /// Design model element this model was parsed from.
        /// </summary>
        IXmlElement Element { get; }
        /// <summary>
        /// Namespace this design model belongs to.
        /// </summary>
        INamespace Namespace { get; }
        /// <summary>
        /// Extra data for this design model.
        /// </summary>
        Dictionary<string, object> CustomData { get; }
        /// <summary>
        /// Output configuration to be used by output models created from this design model.
        /// </summary>
        IOutputConfiguration OutputConfiguration { get; }
        /// <summary>
        /// Returns all output models registered for this design model.
        /// </summary>
        IEnumerable<IOutputModel> OutputModels { get; }
        /// <summary>
        /// Registers an output model for this design model.
        /// </summary>
        /// <param name="outputModel"></param>
        void AddOutputModel(IOutputModel outputModel);
    }
}
