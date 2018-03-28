using System;
using System.Collections.Generic;
using System.Text;
using Polygen.Core.Project;

namespace Polygen.Core.OutputModel
{
    /// <summary>
    /// Interface for merging an existing file contents with the generated output file.
    /// </summary>
    public interface IOutputModelMerger
    {
        /// <summary>
        /// Executes the merge operation.
        /// </summary>
        /// <param name="projectFile"></param>
        void MergeFile(IProjectFile projectFile);
    }
}
