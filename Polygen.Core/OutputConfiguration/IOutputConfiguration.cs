using System.Collections;
using System.Collections.Generic;
using Polygen.Core.DesignModel;
using Polygen.Core.NamingConvention;
using Polygen.Core.Project;
using Polygen.Core.TargetPlatform;
using Polygen.Core.Utils;

namespace Polygen.Core.OutputConfiguration
{
    /// <summary>
    /// Interface for configuration used to generate the output models and out file content.
    /// Configurations can be defined as a hierarchy where the more specific levels (e.g. project or namespace)
    /// inhertit configration from the parent leve.
    /// </summary>
    public interface IOutputConfiguration
    {
        /// <summary>
        /// Parent level configuration.
        /// </summary>
        IOutputConfiguration Parent { get; }
        /// <summary>
        /// Returns target platforms for the given design model.
        /// </summary>
        /// <param name="designModel"></param>
        /// <returns></returns>
        IEnumerable<ITargetPlatform> GetTargetPlatformsForDesignModel(IDesignModel designModel);
        /// <summary>
        /// Registers a target platform for the given design model type.
        /// </summary>
        /// <param name="designModelType"></param>
        /// <param name="targetPlatform"></param>
        /// <param name="replace"></param>
        void RegisterTargetPlatformForDesignModelType(string designModelType, ITargetPlatform targetPlatform, bool replace = false);
        /// <summary>
        /// Returns the output folder for the given output model type.
        /// </summary>
        /// <param name="outputModelType"></param>
        /// <param name="throwIfMissing"></param>
        /// <returns></returns>
        IProjectFolder GetOutputFolder(string outputModelType, bool throwIfMissing = true);
        /// <summary>
        /// Registers the output folder for the given output model type. The model type can contain wildcards, 
        /// e.g. "Entity/*" or "**/*Generated"
        /// </summary>
        /// <param name="outputModelTypeFilter"></param>
        /// <param name="projectFolder"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        void RegisterOutputFolder(Filter outputModelTypeFilter, IProjectFolder projectFolder, bool overwrite = true);
    }
}
