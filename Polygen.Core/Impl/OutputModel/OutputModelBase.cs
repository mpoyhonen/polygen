using Polygen.Core.DesignModel;
using Polygen.Core.OutputConfiguration;
using Polygen.Core.OutputModel;
using Polygen.Core.Project;
using Polygen.Core.Renderer;
using System.Collections.Generic;

namespace Polygen.Core.Impl.OutputModel
{
    /// <summary>
    /// Base class for output models. Can also be used as a container for generic output models.
    /// </summary>
    public class OutputModelBase : IOutputModel
    {
        private IEnumerable<IOutputModelFragment> _fragments;

        public OutputModelBase(string type, INamespace ns, IDesignModel designModel = null, IProjectFile file = null)
        {
            this.Type = type;
            this.Namespace = ns;
            this.DesignModel = designModel;
            this.File = file;
            this.OutputConfiguration = new OutputConfiguration.OutputConfiguration(this.Namespace?.OutputConfiguration);
        }

        public string Type { get; set; }
        public IDesignModel DesignModel { get; set; }
        public INamespace Namespace { get; set; }
        public IProjectFile File { get; set; }
        public IOutputModelRenderer Renderer { get; set; }
        public IEnumerable<IOutputModelFragment> Fragments => this._fragments ?? (this._fragments = new List<IOutputModelFragment>());
        public IOutputConfiguration OutputConfiguration { get; }
    }
}
