using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygen.Core.Renderer
{
    /// <summary>
    /// Interface for a registry for output model and output model fragment renderers.
    /// </summary>
    public interface IOutputModelRendererRegistry
    {
        void RegisterOutputModelRenderer(string type, IOutputModelRenderer renderer);
        void RegisterOutputModelFragmentRenderer(string type, IOutputModelFragmentRenderer renderer);

        IOutputModelRenderer GetOutputModelRenderer(string type, bool throwIfMissing = true);
        IOutputModelFragmentRenderer GetOutputModelFragmentRenderer(string type, bool throwIfMissing = true);
    }
}
