using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polygen.Core;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Core.Stage;
using Polygen.Core.TargetPlatform;
using Polygen.Core.Template;
using Polygen.Core.Utils;
using Polygen.Plugins.Base;

namespace Polygen.Plugins.Entity.StageHandler
{
    /// <summary>
    /// Initializes the output model configuration with defaults.
    /// </summary>
    public class InitializeOutputConfiguration : StageHandlerBase
    {
        public InitializeOutputConfiguration() : base(StageType.InitializeOutputConfiguration, "Entity", "Base")
        {
        }

        public ITargetPlatformCollection TargetPlatformCollection { get; set; }
        public ITemplateCollection TemplateCollection { get; set; }
        public IProjectCollection Projects { get; set; }
        public IDesignModelCollection DesignModelCollection { get; set; }

        public override void Execute()
        {
            this.TemplateCollection.LoadTemplates(this.GetType().Assembly);

            var dataProject = this.Projects.GetFirstProjectByType(BasePluginConstants.ProjectType_Data);
            var mainOutputConfiguration = this.DesignModelCollection.RootNamespace.OutputConfiguration;

            mainOutputConfiguration.RegisterTargetPlatformForDesignModelType(EntityPluginConstants.DesignModelType_Entity, TargetPlatformCollection.GetTargetPlatform("EntityTest"));
            mainOutputConfiguration.RegisterOutputFolder(new Filter(EntityPluginConstants.OutputModelType_Entity_GeneratedClass), dataProject.GetFolder("Entity"));
            mainOutputConfiguration.RegisterOutputFolder(new Filter(EntityPluginConstants.OutputModelType_Entity_CustomClass), dataProject.GetFolder("Entity"));

            mainOutputConfiguration.RegisterOutputFolder(new Filter(EntityPluginConstants.OutputModelType_Entity_CustomClass), dataProject.GetFolder("Entity"));
        }
    }
}
