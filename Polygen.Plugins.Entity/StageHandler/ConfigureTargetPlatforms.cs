using Polygen.Core.Impl.TargetPlatform;
using Polygen.Core.NamingConvention;
using Polygen.Core.Stage;
using Polygen.Core.TargetPlatform;
using Polygen.Core.Template;
using Polygen.Core.Utils;
using Polygen.Plugins.Base;
using Polygen.Plugins.Entity.Output.Backend;

namespace Polygen.Plugins.Entity.StageHandler
{
    /// <summary>
    /// Creates output mopdels from the desing models.
    /// </summary>
    public class ConfigureTargetPlatforms: StageHandlerBase
    {
        public ConfigureTargetPlatforms(): base(StageType.ConfigureTargetPlatforms, "Entity")
        {
        }

        public ITargetPlatformCollection TargetPlatformCollection { get; set; }
        public ITemplateCollection TemplateCollection { get; set; }
        public INamingConventionCollection NamingConventionCollection { get; set; }
        public EntityOutputModelGenerator EntityOutputModelGenerator { get; set; }

        public override void Execute()
        {
            // This is for the test cases. We need to move this code to the NHibernate, etc. plugins.
            var targetPlatform = new TargetPlatform("EntityTest") as ITargetPlatform;

            targetPlatform.RegisterClassNamingConvention(BasePluginConstants.Language_CSharp, this.NamingConventionCollection.GetClassNamingConvention(BasePluginConstants.Language_CSharp));
            targetPlatform.RegisterOutputModelGenerator(EntityPluginConstants.DesignModelType_Entity, this.EntityOutputModelGenerator, overwrite: true);
            targetPlatform.RegisterOutputTemplate(EntityPluginConstants.OutputModelType_Entity_GeneratedClass, this.TemplateCollection.GetTemplate(EntityPluginConstants.OutputTemplateName_Entity_GeneratedClass));
            targetPlatform.RegisterOutputTemplate(EntityPluginConstants.OutputModelType_Entity_CustomClass, this.TemplateCollection.GetTemplate(EntityPluginConstants.OutputTemplateName_Entity_CustomClass));

            this.TargetPlatformCollection.RegisterTargetPlatform(targetPlatform);
        }
    }
}
