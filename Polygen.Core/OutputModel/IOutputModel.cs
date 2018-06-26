using Polygen.Core.DesignModel;
using Polygen.Core.OutputConfiguration;
using Polygen.Core.Project;
using Polygen.Core.Renderer;
using System.Collections.Generic;
using Polygen.Core.File;

namespace Polygen.Core.OutputModel
{
    /// <summary>
    /// Inteface for a output data created from design models. This will be used to generate an
    /// output file using a content renderer.
    /// </summary>
    public interface IOutputModel
    {
        /// <summary>
        /// Defines the output model type.
        /// </summary>
        string Type { get; }

        /// <summary>
        /// The namespace this output model belongs to. Usually this is the design model namespace.
        /// </summary>
        INamespace Namespace { get; }

        /// <summary>
        /// Design model this output model is based one. Can be null.
        /// </summary>
        IDesignModel DesignModel { get; }

        /// <summary>
        /// Output configuration to be used by this output model.
        /// </summary>
        IOutputConfiguration OutputConfiguration { get; }

        /// <summary>
        /// Output file which will be created from this model.
        /// </summary>
        IProjectFile File { get; set; }

        /// <summary>
        /// Determines how the output model file will be copied to the project.
        /// </summary>
        OutputModelMergeMode MergeMode { get; set; }
        /// <summary>
        /// Renderer for this output model. Can be changed by plugins.
        /// </summary>
        IOutputModelRenderer Renderer { get; set; }

        /// <summary>
        /// Contains the output fragments. These are passed to the output fragment renderer.
        /// </summary>
        IEnumerable<IOutputModelFragment> Fragments { get; }
    }
}
