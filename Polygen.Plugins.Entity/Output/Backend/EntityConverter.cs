using System.Linq;
using Polygen.Common.Class.OutputModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Template;

namespace Polygen.Plugins.Entity.Output.Backend
{
    /// <summary>
    /// Converts entities into backend output model.
    /// </summary>
    public class EntityConverter
    {
        private ITemplateCollection _templateCollection;

        public EntityConverter(ITemplateCollection templateCollection)
        {
            this._templateCollection = templateCollection;
        }

        public ClassOutputModel CreateEntityGeneratedClass(DesignModel.Entity entity, string language)
        {

            var targetPlatform = entity.OutputConfiguration.GetTargetPlatformsForDesignModel(entity).FirstOrDefault();

            if (targetPlatform == null)
            {
                throw new ConfigurationException($"No target platforms defined for design model type '{entity.Type}'.");
            }

            var namingConvention = targetPlatform.GetClassNamingConvention(language);
            var outputModelType = EntityPluginConstants.OutputModelType_Entity_GeneratedClass;
            var builder = new ClassOutputModelBuilder(outputModelType, entity, namingConvention);

            builder.CreatePartialClass(entity.Name, entity.Namespace);
            builder.SetOutputFile(entity.OutputConfiguration, namingConvention, fileExtension: ".gen.cs");
            builder.SetOutputRenderer(targetPlatform.GetOutputTemplate(outputModelType));

            foreach (var entityAttribute in entity.Attributes)
            {
                builder.CreateProperty(entityAttribute.Name, entityAttribute.Type);
            }

            return builder.Build();
        }

        public ClassOutputModel CreateEntityCustomClass(DesignModel.Entity entity, string language)
        {
            var targetPlatform = entity.OutputConfiguration.GetTargetPlatformsForDesignModel(entity).FirstOrDefault();

            if (targetPlatform == null)
            {
                throw new ConfigurationException($"No target platforms defined for design model type '{entity.Type}'.");
            }

            var namingConvention = targetPlatform.GetClassNamingConvention(language);
            var outputModelType = EntityPluginConstants.OutputModelType_Entity_CustomClass;
            var builder = new ClassOutputModelBuilder(outputModelType, entity, namingConvention);

            builder.CreatePartialClass(entity.Name, entity.Namespace);
            builder.SetOutputFile(entity.OutputConfiguration, namingConvention, fileExtension: ".cs");
            builder.SetOutputRenderer(targetPlatform.GetOutputTemplate(outputModelType));

            return builder.Build();
        }
    }
}
