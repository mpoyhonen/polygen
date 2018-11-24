using System.Linq;
using Polygen.Common.Class.OutputModel;
using Polygen.Core.Exceptions;
using Polygen.Core.OutputModel;
using Polygen.Core.Template;

namespace Polygen.Plugins.NHibernate.Output.Entity
{
    /// <summary>
    /// Converts entities into backend output model.
    /// </summary>
    public class EntityConverter
    {
        private ITemplateCollection _templateCollection;

        public EntityConverter(ITemplateCollection templateCollection)
        {
            _templateCollection = templateCollection;
        }

        public ClassOutputModel CreateEntityGeneratedClass(Base.Models.Entity.Entity entity, string language)
        {
            var targetPlatform = entity.OutputConfiguration.GetTargetPlatformsForDesignModel(entity).FirstOrDefault();

            if (targetPlatform == null)
            {
                throw new ConfigurationException(
                    $"No target platforms defined for design model type '{entity.DesignModelType}'.");
            }

            var namingConvention = targetPlatform.GetClassNamingConvention(language);
            var outputModelType = NHibernatePluginConstants.OutputModelType_Entity_GeneratedClass;
            var builder = new ClassOutputModelBuilder(outputModelType, entity, namingConvention);

            builder.CreatePartialClass(entity.Name, entity.Namespace);
            builder.SetOutputFile(entity.OutputConfiguration, namingConvention, fileExtension: ".gen.cs",
                mergeMode: OutputModelMergeMode.Replace);
            builder.SetOutputRenderer(targetPlatform.GetOutputTemplate(outputModelType));

            foreach (var entityAttribute in entity.Attributes)
            {
                builder.CreateProperty(entityAttribute.Name, namingConvention.GetTypeName(entityAttribute.Type));
            }

            return builder.Build();
        }

        public ClassOutputModel CreateEntityCustomClass(Base.Models.Entity.Entity entity, string language)
        {
            var targetPlatform = entity.OutputConfiguration.GetTargetPlatformsForDesignModel(entity).FirstOrDefault();

            if (targetPlatform == null)
            {
                throw new ConfigurationException(
                    $"No target platforms defined for design model type '{entity.DesignModelType}'.");
            }

            var namingConvention = targetPlatform.GetClassNamingConvention(language);
            var outputModelType = NHibernatePluginConstants.OutputModelType_Entity_CustomClass;
            var builder = new ClassOutputModelBuilder(outputModelType, entity, namingConvention);

            builder.CreatePartialClass(entity.Name, entity.Namespace);
            builder.SetOutputFile(entity.OutputConfiguration, namingConvention, fileExtension: ".cs");
            builder.SetOutputRenderer(targetPlatform.GetOutputTemplate(outputModelType));

            return builder.Build();
        }
    }
}
