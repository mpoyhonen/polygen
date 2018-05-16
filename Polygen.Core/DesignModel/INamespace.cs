using Polygen.Core.OutputConfiguration;
using System.Collections.Generic;
using Polygen.Core.Parser;
using Polygen.Core.Reference;

namespace Polygen.Core.DesignModel
{
    /// <summary>
    /// Interface for an output model namespace.
    /// </summary>
    public interface INamespace
    {
        /// <summary>
        /// Full name of the namespace.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Name of the this namespace segment.
        /// </summary>
        string SegmentName { get; }
        /// <summary>
        /// Parent namespace.
        /// </summary>
        INamespace Parent { get; }
        /// <summary>
        /// Output configuration to be used by all output models under this namespace.
        /// </summary>
        IOutputConfiguration OutputConfiguration { get; }
        /// <summary>
        /// Child namespaces.
        /// </summary>
        IReadOnlyList<INamespace> Children { get; }
        /// <summary>
        /// Design models defined in this namespace.
        /// </summary>
        IEnumerable<IDesignModel> DesignModels { get; }
        /// <summary>
        /// Adds a design model to this namespace.
        /// </summary>
        /// <param name="designModel"></param>
        void AddDesignModel(IDesignModel designModel);
        /// <summary>
        /// Returns all design models of the given type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        IEnumerable<IDesignModel> FindDesignModelsByType(string type, bool recursive = true);
        /// <summary>
        /// Returns a design model.
        /// </summary>
        /// <param name="reference">Design model reference to resolve. If this contains the a reference to the design model, it is returned.</param>
        /// <param name="throwIfNotFound">Whether to throw an exception when the design model was not found.</param>
        /// <returns></returns>
        T GetDesignModel<T>(DesignModelReference<T> reference, bool throwIfNotFound) where T: IDesignModel;
    }
}
