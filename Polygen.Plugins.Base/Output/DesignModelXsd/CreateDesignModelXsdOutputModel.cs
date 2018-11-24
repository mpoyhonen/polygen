using Polygen.Common.Xml;
using Polygen.Core.OutputModel;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.Output.Xsd;

namespace Polygen.Plugins.Base.Output.DesignModelXsd
{
    /// <summary>
    /// Creates output model for design model XSD.
    /// </summary>
    public class CreateDesignModelXsdOutputModel: StageHandlerBase
    {
        public CreateDesignModelXsdOutputModel(): base(StageType.GenerateOutputModels, nameof(CreateDesignModelXsdOutputModel))
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IProjectCollection Projects { get; set; }
        public IOutputModelCollection OutputModels { get; set; }

        public override void Execute()
        {
            var designProject = Projects.GetFirstProjectByType(BasePluginConstants.ProjectType_Design);
            var schemaConverter = new SchemaConverter();
            var designModelSchema = Schemas.GetSchemaByName(BasePluginConstants.DesignModel_SchemaName);
            var designModelSchemaOutputModel = schemaConverter.Convert(designModelSchema);

            designModelSchemaOutputModel.Renderer = new XmlOutputModelRenderer();
            designModelSchemaOutputModel.File = designProject.GetFile("Schemas/XSD/DesignModels.xsd");
            OutputModels.AddOutputModel(designModelSchemaOutputModel);
        }
    }
}
