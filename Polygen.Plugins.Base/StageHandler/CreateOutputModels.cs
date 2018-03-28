using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polygen.Common.Xml;
using Polygen.Core.DesignModel;
using Polygen.Core.OutputModel;
using Polygen.Core.Parser;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Plugins.Base.Output.Xsd;

namespace Polygen.Plugins.Base.StageHandler
{
    /// <summary>
    /// Creates output mopdels from the desing models.
    /// </summary>
    public class CreateOutputModels: StageHandlerBase
    {
        public CreateOutputModels(): base(StageType.GenerateOutputModels, "Base")
        {
        }

        public ISchemaCollection Schemas { get; set; }
        public IProjectCollection Projects { get; set; }
        public IOutputModelCollection OutputModels { get; set; }

        public override void Execute()
        {
            var designProject = this.Projects.GetFirstProjectByType(BasePluginConstants.ProjectType_Design);
            var schemaConverter = new SchemaConverter();
            var projectConfigurationSchema = this.Schemas.GetSchemaByName(Core.CoreConstants.ProjectConfiguration_SchemaName);
            var projectConfigurationSchemaOutputModel = schemaConverter.Convert(projectConfigurationSchema);

            projectConfigurationSchemaOutputModel.Renderer = new XmlOutputModelRenderer();
            projectConfigurationSchemaOutputModel.File = designProject.GetFile("Schemas/XSD/ProjectConfiguration.xsd");
            this.OutputModels.AddOutputModel(projectConfigurationSchemaOutputModel);

            var designModelSchema = this.Schemas.GetSchemaByName(BasePluginConstants.DesignModel_SchemaName);
            var designModelSchemaOutputModel = schemaConverter.Convert(designModelSchema);

            designModelSchemaOutputModel.Renderer = new XmlOutputModelRenderer();
            designModelSchemaOutputModel.File = designProject.GetFile("Schemas/XSD/DesignModels.xsd");
            this.OutputModels.AddOutputModel(designModelSchemaOutputModel);
        }
    }
}
