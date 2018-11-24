using Polygen.Core.DesignModel;
using Polygen.Core.Project;
using Polygen.Core.Stage;
using Polygen.Core.TargetPlatform;
using Polygen.Core.Template;
using Polygen.Core.Utils;
using Polygen.Plugins.Base;

namespace Polygen.Plugins.NHibernate.StageHandler
{
    /// <summary>
    /// Initializes the output model configuration with defaults.
    /// </summary>
    public class InitializeOutputConfiguration : StageHandlerBase
    {
        public InitializeOutputConfiguration() : base(StageType.InitializeOutputConfiguration, "NHibernate")
        {
        }

        public ITargetPlatformCollection TargetPlatformCollection { get; set; }
        public ITemplateCollection TemplateCollection { get; set; }
        public IProjectCollection Projects { get; set; }
        public IDesignModelCollection DesignModelCollection { get; set; }

        public override void Execute()
        {
            var dataProject = Projects.GetFirstProjectByType(BasePluginConstants.ProjectType_Data);
            var mainOutputConfiguration = DesignModelCollection.RootNamespace.OutputConfiguration;

            mainOutputConfiguration.RegisterTargetPlatformForDesignModelType(BasePluginConstants.DesignModelType_Entity, TargetPlatformCollection.GetTargetPlatform("NHibernate"));
            mainOutputConfiguration.RegisterOutputFolder(new Filter(NHibernatePluginConstants.OutputModelType_Entity_GeneratedClass), dataProject.GetFolder("Entity"));
            mainOutputConfiguration.RegisterOutputFolder(new Filter(NHibernatePluginConstants.OutputModelType_Entity_CustomClass), dataProject.GetFolder("Entity"));

            mainOutputConfiguration.RegisterOutputFolder(new Filter(NHibernatePluginConstants.OutputModelType_Mapping), dataProject.GetFolder("Mapping"));
        }
    }
}
