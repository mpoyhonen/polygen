using System;
using System.Collections.Generic;
using System.Text;
using Polygen.Core.DesignModel;

namespace Polygen.Core.OutputModel
{
    /// <summary>
    /// Interface for generating output models from a design model. This
    /// generator is implemented for each target platform as the output
    /// models are tightly linked to the output templates and naming conventions.
    /// </summary>
    public interface IOutputModelGenerator
    {
        /// <summary>
        /// Generate output models from the given design model.
        /// </summary>
        /// <param name="designModel"></param>
        /// <returns></returns>
        IEnumerable<IOutputModel> GenerateOutputModels(IDesignModel designModel);
    }
}
