using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygen.Core.OutputModel;

namespace Polygen.Core.Renderer
{
    /// <summary>
    /// Interface for converting output models into output file contents.
    /// </summary>
    public interface IOutputModelRenderer
    {
        /// <summary>
        /// Renders the given output model and writes the result into the given writer.
        /// </summary>
        /// <param name="outputModel"></param>
        /// <param name="writer"></param>
        void Render(IOutputModel outputModel, TextWriter writer);
    }
}
