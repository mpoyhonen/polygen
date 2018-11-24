using Polygen.Common.Xml;
using Polygen.Core.OutputModel;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.Output.Xsd;

namespace Polygen.Plugins.Base.Output.ProjectConfigurationXsd
{
    /// <summary>
    /// Creates output model for project configuration XSD.
    /// </summary>
    public class CreateProjectConfigurationXsdOutputModel: StageHandlerBase
    {
        public CreateProjectConfigurationXsdOutputModel(): base(StageType.GenerateOutputModels, nameof(CreateProjectConfigurationXsdOutputModel))
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IProjectCollection Projects { get; set; }
        public IOutputModelCollection OutputModels { get; set; }

        public override void Execute()
        {
            var designProject = Projects.GetFirstProjectByType(BasePluginConstants.ProjectType_Design);
            var schemaConverter = new SchemaConverter();
            var projectConfigurationSchema = Schemas.GetSchemaByName(Core.CoreConstants.ProjectConfiguration_SchemaName);
            var projectConfigurationSchemaOutputModel = schemaConverter.Convert(projectConfigurationSchema);

            projectConfigurationSchemaOutputModel.File = designProject.GetFile("Schemas/XSD/ProjectConfiguration.xsd");
            OutputModels.AddOutputModel(projectConfigurationSchemaOutputModel);
        }
    }
}
