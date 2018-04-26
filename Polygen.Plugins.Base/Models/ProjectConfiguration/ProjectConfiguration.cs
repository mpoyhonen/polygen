using Polygen.Core.DesignModel;
using Polygen.Core.Impl.DesignModel;
using Polygen.Core.Parser;
using Polygen.Core.Project;

namespace Polygen.Plugins.Base.Models.ProjectConfiguration
{
    /// <summary>
    /// Contains the project configuration parsed from design models defined for ProjectConfiguration schema.
    /// This extends the DesignModel object, so that plugins can extract any non-standard properties from it.
    /// </summary>
    public class ProjectConfiguration : DesignModelBase, IProjectConfiguration
    {
        public ProjectConfiguration(string type, INamespace ns, IXmlElement designModelElement, IProjectCollection projects)
            : base(type, ns, designModelElement)
        {
            this.Projects = projects;
        }

        public string ProjectName { get; }

        public IProjectCollection Projects { get; }
    }
}
