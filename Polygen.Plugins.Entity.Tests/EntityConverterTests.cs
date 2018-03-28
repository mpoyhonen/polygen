using Polygen.Core.Impl.DesignModel;
using Polygen.Plugins.Base;
using Polygen.Plugins.Base.NamingConvention;
using Polygen.Plugins.Entity.Output.Backend;
using Polygen.Templates.HandlebarsNet;
using FluentAssertions;
using System.Linq;
using Xunit;
using Polygen.Core.Impl.Project;
using Polygen.Core.Utils;
using Polygen.Core.Impl.TargetPlatform;

namespace Polygen.Plugins.Entity.Tests
{
    public class EntityConverterTests
    {
        [Fact]
        public void Test_entity_conversion_to_generated_class_output_model()
        {
            var ns = new Namespace("MyApp.MyTest", null);
            var entity = new DesignModel.Entity(ns)
            {
                Name = "MyClass"
            };
            var targetPlatform = new TargetPlatform("test");

            targetPlatform.RegisterClassNamingConvention(BasePluginConstants.Language_CSharp, new CSharpClassNamingConvention());

            entity.AddAttribute(new DesignModel.EntityAttribute(entity, "Id", "int"));
            entity.AddAttribute(new DesignModel.EntityAttribute(entity, "Name", "string"));
            entity.OutputConfiguration.RegisterOutputFolder(new Filter(EntityPluginConstants.OutputModelType_Entity_GeneratedClass), new ProjectFolder(""));
            entity.OutputConfiguration.RegisterTargetPlatformForDesignModelType(entity.Type, targetPlatform);

            var templateCollection = new TemplateCollection();

            templateCollection.LoadTemplates(typeof(EntityPluginConstants).Assembly);

            var template = templateCollection.GetTemplate(EntityPluginConstants.OutputTemplateName_Entity_GeneratedClass);

            targetPlatform.RegisterOutputTemplate(EntityPluginConstants.OutputTemplateName_Entity_GeneratedClass, template);

            var converter = new EntityConverter(templateCollection);
            var generatedClass = converter.CreateEntityGeneratedClass(entity, BasePluginConstants.Language_CSharp);

            generatedClass.Should().NotBeNull();
            generatedClass.ClassName.ShouldBeEquivalentTo("MyClass");
            generatedClass.ClassNamespace.ShouldBeEquivalentTo("MyApp.MyTest");

            var properties = generatedClass
                .Properties
                .Select(x => (name: x.Name, type: x.Type.TypeName));

            properties.ShouldBeEquivalentTo(new[]
            {
                (name: "Id", type: "int"),
                (name: "Name", type: "string")
            });
        }

        [Fact]
        public void Test_entity_conversion_to_custom_class_output_model()
        {
            var ns = new Namespace("MyApp.MyTest", null);
            var entity = new DesignModel.Entity(ns)
            {
                Name = "MyClass"
            };
            var targetPlatform = new TargetPlatform("test");

            targetPlatform.RegisterClassNamingConvention(BasePluginConstants.Language_CSharp, new CSharpClassNamingConvention());

            entity.AddAttribute(new DesignModel.EntityAttribute(entity, "Id", "int"));
            entity.AddAttribute(new DesignModel.EntityAttribute(entity, "Name", "string"));
            entity.OutputConfiguration.RegisterOutputFolder(new Filter(EntityPluginConstants.OutputModelType_Entity_GeneratedClass), new ProjectFolder(""));
            entity.OutputConfiguration.RegisterOutputFolder(new Filter(EntityPluginConstants.OutputModelType_Entity_CustomClass), new ProjectFolder(""));
            entity.OutputConfiguration.RegisterTargetPlatformForDesignModelType(entity.Type, targetPlatform);

            var templateCollection = new TemplateCollection();

            templateCollection.LoadTemplates(typeof(EntityPluginConstants).Assembly);

            var generatedClassTemplate = templateCollection.GetTemplate(EntityPluginConstants.OutputTemplateName_Entity_GeneratedClass);
            var customClassTemplate = templateCollection.GetTemplate(EntityPluginConstants.OutputTemplateName_Entity_CustomClass);

            targetPlatform.RegisterOutputTemplate(EntityPluginConstants.OutputTemplateName_Entity_GeneratedClass, generatedClassTemplate);
            targetPlatform.RegisterOutputTemplate(EntityPluginConstants.OutputTemplateName_Entity_CustomClass, generatedClassTemplate);

            var converter = new EntityConverter(templateCollection);
            var generatedClass = converter.CreateEntityCustomClass(entity, BasePluginConstants.Language_CSharp);

            generatedClass.Should().NotBeNull();
            generatedClass.ClassName.ShouldBeEquivalentTo("MyClass");
            generatedClass.ClassNamespace.ShouldBeEquivalentTo("MyApp.MyTest");
            generatedClass.Properties.Count.ShouldBeEquivalentTo(0);
        }
    }
}
