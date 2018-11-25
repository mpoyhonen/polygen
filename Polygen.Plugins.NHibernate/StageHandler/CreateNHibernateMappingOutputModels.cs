using System.ComponentModel.DataAnnotations;
using System.Linq;
using Polygen.Common.Xml;
using Polygen.Core.DesignModel;
using Polygen.Core.NamingConvention;
using Polygen.Core.OutputModel;
using Polygen.Core.Project;
using Polygen.Core.Stage;
using Polygen.Plugins.Base;
using Polygen.Plugins.NHibernate.Output.Entity;
using Polygen.Plugins.NHibernate.Output.Hbm;

namespace Polygen.Plugins.NHibernate.StageHandler
{
    /// <summary>
    /// Creates HBM mapping XML output models for all entities. These are
    /// grouped by project and namespace.
    /// </summary>
    public class CreateNHibernateMappingOutputModels: StageHandlerBase
    {
        public CreateNHibernateMappingOutputModels(): base(StageType.GenerateOutputModels, "NHibernateMapping")
        {
        }

        public IDesignModelCollection DesignModels { get; set; }
        public IProjectCollection Projects { get; set; }
        public IOutputModelCollection OutputModels { get; set; }
        public INamingConventionCollection NamingConventionCollection { get; set; }

        public override void Execute()
        {
            var hbmConverter = new HbmMappingConverter();
            var namingConvention = (IDatabaseNamingConvention) NamingConventionCollection.GetNamingConvention(NHibernatePluginConstants.Language_SQL);
            var designsModelByOutputFolderAndNamespace = DesignModels.RootNamespace.FindDesignModelsByType("Entity")
                .Cast<Base.Models.Entity.Entity>()
                .GroupBy(x => new
                {
                    OutputFolder = x.OutputConfiguration.GetOutputFolder(NHibernatePluginConstants.OutputModelType_Mapping),
                    x.Namespace
                });

            foreach (var group in designsModelByOutputFolderAndNamespace)
            {
                var outputFolder = group.Key.OutputFolder;
                var project = outputFolder.Project;
                var ns = group.Key.Namespace;
                var outputModel = hbmConverter.Convert(namingConvention, project, ns, group.OrderBy(x => x.Name));
                
                outputModel.Renderer = new XmlOutputModelRenderer();
                outputModel.File = outputFolder.GetFile($"{project.Name}.Entity.hbm.xml"); // TODO: Get this from configuration?
                OutputModels.AddOutputModel(outputModel);                
            }
        }

        public EntityConverter EntityConverter { get; set; }
    }
}
