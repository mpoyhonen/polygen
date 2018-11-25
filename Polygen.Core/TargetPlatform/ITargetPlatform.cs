using System;
using System.Collections.Generic;
using System.Text;
using Polygen.Core.DesignModel;
using Polygen.Core.NamingConvention;
using Polygen.Core.OutputModel;
using Polygen.Core.Template;

namespace Polygen.Core.TargetPlatform
{
    /// <summary>
    /// Interface to define target platform (e.g. NHibernate) output configuration.
    /// This defines everything needed to create output models and
    /// generated code for one platform.
    /// </summary>
    public interface ITargetPlatform
    {
        /// <summary>
        /// Target platform name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Returns the output model generator for the given design model type.
        /// </summary>
        /// <param name="designModelType"></param>
        /// <param name="throwIfMissing"></param>
        /// <returns></returns>
        IOutputModelGenerator GetOutputModelGenerator(string designModelType, bool throwIfMissing = true);
        /// <summary>
        /// Registers an output model generator for the given design model type.
        /// </summary>
        /// <param name="designModelType"></param>
        /// <param name="outputModelGenerator"></param>
        /// <param name="overwrite"></param>
        void RegisterOutputModelGenerator(string designModelType, IOutputModelGenerator outputModelGenerator, bool overwrite = false);

        /// <summary>
        /// Gets the naming convention to use for the given output language.
        /// </summary>
        /// <param name="language"></param>
        /// <param name="throwIfMissing"></param>
        /// <returns></returns>
        INamingConvention GetNamingConvention(string language, bool throwIfMissing = true);

        /// <summary>
        /// Registers the naming convention for the given output language.
        /// </summary>
        /// <param name="language"></param>
        /// <param name="namingConvention"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        void RegisterNamingConvention(string language, INamingConvention namingConvention, bool overwrite = true);
        /// <summary>
        /// Returns the output template for the given output model type.
        /// </summary>
        /// <param name="outputModelType"></param>
        /// <param name="throwIfMissing"></param>
        /// <returns></returns>
        ITemplate GetOutputTemplate(string outputModelType, bool throwIfMissing = true);
        /// <summary>
        /// Registers the output template for the given output model type.
        /// </summary>
        /// <param name="outputModelType"></param>
        /// <param name="outputTemplate"></param>
        /// <param name="overwrite"></param>
        /// <returns></returns>
        void RegisterOutputTemplate(string outputModelType, ITemplate outputTemplate, bool overwrite = true);
    }
}
