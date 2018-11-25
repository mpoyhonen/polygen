using Polygen.Core.Impl.TargetPlatform;
using Polygen.Core.NamingConvention;
using Polygen.Core.Stage;
using Polygen.Core.TargetPlatform;
using Polygen.Core.Template;
using Polygen.Plugins.Base;
using Polygen.Plugins.NHibernate.Output.Entity;

namespace Polygen.Plugins.NHibernate.StageHandler
{
    /// <summary>
    /// Creates output mopdels from the desing models.
    /// </summary>
    public class ConfigureTargetPlatforms: StageHandlerBase
    {
        public ConfigureTargetPlatforms(): base(StageType.ConfigureTargetPlatforms, "NHibernate")
        {
        }

        public ITargetPlatformCollection TargetPlatformCollection { get; set; }
        public ITemplateCollection TemplateCollection { get; set; }
        public INamingConventionCollection NamingConventionCollection { get; set; }
        public EntityOutputModelGenerator EntityOutputModelGenerator { get; set; }

        public override void Execute()
        {
            var targetPlatform = new TargetPlatform("NHibernate") as ITargetPlatform;

            targetPlatform.RegisterNamingConvention(BasePluginConstants.Language_CSharp, NamingConventionCollection.GetNamingConvention(BasePluginConstants.Language_CSharp));
            targetPlatform.RegisterOutputModelGenerator(BasePluginConstants.DesignModelType_Entity, EntityOutputModelGenerator, overwrite: true);
            targetPlatform.RegisterOutputTemplate(NHibernatePluginConstants.OutputModelType_Entity_GeneratedClass, TemplateCollection.GetTemplate(NHibernatePluginConstants.OutputTemplateName_Entity_GeneratedClass));
            targetPlatform.RegisterOutputTemplate(NHibernatePluginConstants.OutputModelType_Entity_CustomClass, TemplateCollection.GetTemplate(NHibernatePluginConstants.OutputTemplateName_Entity_CustomClass));

            TargetPlatformCollection.RegisterTargetPlatform(targetPlatform);
        }
    }
}
