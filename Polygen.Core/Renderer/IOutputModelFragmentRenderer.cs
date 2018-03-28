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
    /// Interface for converting output model fragments into output file contents inside on output model.
    /// </summary>
    public interface IOutputModelFragmentRenderer
    {
        /// <summary>
        /// Renders the given output model and writes the result into the given writer.
        /// </summary>
        /// <param name="outputModel"></param>
        /// <param name="outputModelFragment"></param>
        /// <param name="writer"></param>
        void Render(IOutputModel outputModel, IOutputModelFragment outputModelFragment, TextWriter writer);
    }
}
