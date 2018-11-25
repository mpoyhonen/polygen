using System.Linq;
using FluentAssertions;
using Polygen.Core.Impl.DesignModel;
using Polygen.Core.Impl.Project;
using Polygen.Core.Impl.TargetPlatform;
using Polygen.Core.Utils;
using Polygen.Plugins.Base;
using Polygen.Plugins.Base.Models.Entity;
using Polygen.Plugins.Base.NamingConvention;
using Polygen.Plugins.NHibernate.Output.Entity;
using Polygen.Templates.HandlebarsNet;
using Polygen.TestUtils.DataType;
using Xunit;

namespace Polygen.Plugins.NHibernate.Tests
{
    public class EntityConverterTests
    {
        [Fact]
        public void Test_entity_conversion_to_generated_class_output_model()
        {
            var ns = new Namespace("MyApp.MyTest", null);
            var entity = new Entity("MyClass", ns);
            var targetPlatform = new TargetPlatform("test");

            targetPlatform.RegisterNamingConvention(BasePluginConstants.Language_CSharp, new CSharpClassNamingConvention());

            entity.AddAttribute(new EntityAttribute("Id", TestDataTypes.Int, null, null));
            entity.AddAttribute(new EntityAttribute("Name", TestDataTypes.String, null, null));
            entity.OutputConfiguration.RegisterOutputFolder(new Filter(NHibernatePluginConstants.OutputModelType_Entity_GeneratedClass), new ProjectFolder(""));
            entity.OutputConfiguration.RegisterTargetPlatformForDesignModelType(entity.DesignModelType, targetPlatform);

            var templateCollection = new TemplateCollection();

            templateCollection.LoadTemplates(typeof(NHibernatePluginConstants).Assembly, NHibernatePluginConstants.TemplateResourcePrefix, NHibernatePluginConstants.TemplateNamePrefix);

            var template = templateCollection.GetTemplate(NHibernatePluginConstants.OutputTemplateName_Entity_GeneratedClass);

            targetPlatform.RegisterOutputTemplate(NHibernatePluginConstants.OutputTemplateName_Entity_GeneratedClass, template);

            var converter = new EntityConverter(templateCollection);
            var generatedClass = converter.CreateEntityGeneratedClass(entity, BasePluginConstants.Language_CSharp);

            generatedClass.Should().NotBeNull();
            generatedClass.ClassName.Should().Be("MyClass");
            generatedClass.ClassNamespace.Should().Be("MyApp.MyTest");

            var properties = generatedClass
                .Properties
                .Select(x => (name: x.Name, type: x.Type.TypeName));

            properties.Should().BeEquivalentTo(new[]
            {
                (name: "Id", type: "int"),
                (name: "Name", type: "string")
            });
        }

        [Fact]
        public void Test_entity_conversion_to_custom_class_output_model()
        {
            var ns = new Namespace("MyApp.MyTest", null);
            var entity = new Entity("MyClass", ns);
            var targetPlatform = new TargetPlatform("test");

            targetPlatform.RegisterNamingConvention(BasePluginConstants.Language_CSharp, new CSharpClassNamingConvention());

            entity.AddAttribute(new EntityAttribute("Id", TestDataTypes.Int, null, null));
            entity.AddAttribute(new EntityAttribute("Name", TestDataTypes.String, null, null));
            entity.OutputConfiguration.RegisterOutputFolder(new Filter(NHibernatePluginConstants.OutputModelType_Entity_GeneratedClass), new ProjectFolder(""));
            entity.OutputConfiguration.RegisterOutputFolder(new Filter(NHibernatePluginConstants.OutputModelType_Entity_CustomClass), new ProjectFolder(""));
            entity.OutputConfiguration.RegisterTargetPlatformForDesignModelType(entity.DesignModelType, targetPlatform);

            var templateCollection = new TemplateCollection();

            templateCollection.LoadTemplates(typeof(NHibernatePluginConstants).Assembly, NHibernatePluginConstants.TemplateResourcePrefix, NHibernatePluginConstants.TemplateNamePrefix);

            var generatedClassTemplate = templateCollection.GetTemplate(NHibernatePluginConstants.OutputTemplateName_Entity_GeneratedClass);
            var customClassTemplate = templateCollection.GetTemplate(NHibernatePluginConstants.OutputTemplateName_Entity_CustomClass);

            targetPlatform.RegisterOutputTemplate(NHibernatePluginConstants.OutputTemplateName_Entity_GeneratedClass, generatedClassTemplate);
            targetPlatform.RegisterOutputTemplate(NHibernatePluginConstants.OutputTemplateName_Entity_CustomClass, customClassTemplate);

            var converter = new EntityConverter(templateCollection);
            var generatedClass = converter.CreateEntityCustomClass(entity, BasePluginConstants.Language_CSharp);

            generatedClass.Should().NotBeNull();
            generatedClass.ClassName.Should().Be("MyClass");
            generatedClass.ClassNamespace.Should().Be("MyApp.MyTest");
            generatedClass.Properties.Count.Should().Be(0);
            
            var customClass = converter.CreateEntityCustomClass(entity, BasePluginConstants.Language_CSharp);

            customClass.Should().NotBeNull();
            customClass.ClassName.Should().Be("MyClass");
            customClass.ClassNamespace.Should().Be("MyApp.MyTest");
            customClass.Properties.Count.Should().Be(0);
        }
    }
}
