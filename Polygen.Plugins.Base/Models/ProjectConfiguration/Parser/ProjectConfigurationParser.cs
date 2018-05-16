using System.IO;
using System.Linq;
using Polygen.Core;
using Polygen.Core.DesignModel;
using Polygen.Core.Impl.Project;
using Polygen.Core.Parser;
using Polygen.Core.Project;

namespace Polygen.Plugins.Base.Models.ProjectConfiguration.Parser
{
    /// <summary>
    /// ProjectConfigurationParser design model parser.
    /// </summary>
    public class ProjectConfigurationParser: DesignModelGeneratorBase
    {
        private readonly IProjectCollection _projectCollection;

        public ProjectConfigurationParser(IProjectCollection projectCollection)
        {
            _projectCollection = projectCollection;
        }

        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            var projectConfiguration = new Models.ProjectConfiguration.ProjectConfiguration(
                CoreConstants.DesignModelType_ProjectConfiguration,
                context.Namespace,
                xmlElement,
                _projectCollection
            );
            var configFileDir = Path.GetFullPath(Path.GetDirectoryName(xmlElement.ParseLocation.File.GetSourcePath()));
            var solutionElement = xmlElement.FindChildElement("Solution");
            var solutionPath = solutionElement.GetAttribute("path").Value;

            solutionPath = Path.GetFullPath(Path.Combine(configFileDir, solutionPath));

            foreach (var projectElement in solutionElement.Children.Where(x => x.Definition.Name.LocalName == "Project"))
            {
                var project = new Project(
                    projectElement.GetAttribute("name").Value,
                    projectElement.GetAttribute("type").Value,
                    Path.Combine(solutionPath, projectElement.GetAttribute("path").Value)
                );

                project.SetDesignModelsFolder("DesignModels,Plugins/*/DesignModels");

                projectConfiguration.Projects.AddProject(project);
            }

            return projectConfiguration;
        }
    }
}
