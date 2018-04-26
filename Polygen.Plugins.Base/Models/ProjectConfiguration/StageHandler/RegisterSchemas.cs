using System.IO;
using System.Linq;
using Polygen.Core;
using Polygen.Core.DataType;
using Polygen.Core.Impl.Project;
using Polygen.Core.Parser;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Core.Stage;

namespace Polygen.Plugins.Base.Models.ProjectConfiguration.StageHandler
{
    /// <summary>
    /// Creates output models from the desing models.
    /// </summary>
    public class RegisterSchemas: StageHandlerBase
    {
        public RegisterSchemas(): base(StageType.RegisterSchemas, "Base-ProjectConfiguration")
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IProjectCollection ProjectCollection { get; set; }
        public IDesignModelGeneratorFactory DesignModelConverterFactory { get; set; }
        public IDataTypeRegistry DataTypeRegistry { get; set; }

        public override void Execute()
        {
            var schema = this.Schemas.AddSchema(CoreConstants.ProjectConfiguration_SchemaName, CoreConstants.ProjectConfiguration_SchemaNamespace);
			var stringType = this.DataTypeRegistry.Get(BasePluginConstants.DataType_string);

			schema
                .CreateRootElement("ProjectConfiguration")
                    .CreateElement("Solution", "Defines project solution projects", c => c.IsMandatory = true)
                        .CreateAttribute("path", stringType, "Solution path relative to this file", c => c.IsMandatory = true)
                        .CreateElement("Project", "Defines a project", c => c.AllowMultiple = true)
                            .CreateAttribute("name", stringType, "Project name", c => c.IsMandatory = true)
                            .CreateAttribute("path", stringType, "Project location relative to the solution path", c => c.IsMandatory = true)
                            .CreateAttribute("type", stringType, "Project type", c => c.IsMandatory = true)
                            .Parent()
                        .CreateElement("Namespaces", "Defines the namespaces to be used in design models");

            this.DesignModelConverterFactory.RegisterFactory(schema.RootElement, this.ParseProjectConfiguration, TODO);
        }

        private ProjectConfiguration ParseProjectConfiguration(IXmlElement projectConfigurationElement, DesignModelParseContext parseContext)
        {
            var projectConfiguration = new ProjectConfiguration(
                CoreConstants.DesignModelType_ProjectConfiguration,
                parseContext.Namespace,
                projectConfigurationElement,
                this.ProjectCollection
            );
            var configFileDir = Path.GetFullPath(Path.GetDirectoryName(projectConfigurationElement.ParseLocation.File.GetSourcePath()));
            var solutionElement = projectConfigurationElement.FindChildElement("Solution");
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
