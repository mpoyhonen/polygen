using Polygen.Core.Tests;
using FluentAssertions;
using Xunit;

namespace Polygen.Templates.HandlebarsNet.Tests
{
    public class TemplateCollectionTests
    {
        [Fact]
        public void Test_template_registration()
        {
            var collection = new TemplateCollection();

            collection.AddTemplate("test", "Hi {{User.Name}}");

            var template = collection.GetTemplate("test");

            template.Should().NotBeNull();
            template.Name.ShouldBeEquivalentTo("test");
            template.RenderIntoString(new
            {
                User = new
                {
                    Name = "Test123"
                }
            }).ShouldBeEquivalentTo("Hi Test123");
        }

        [Fact]
        public void Test_template_loading_from_root()
        {
            using (var tempFolder = new TempFolder())
            {
                var collection = new TemplateCollection();

                tempFolder.CreateWriteTextFile("template-a.hbs", "Contents A");
                tempFolder.CreateWriteTextFile("folder/template-b.hbs", "Contents B");

                collection.LoadTemplates(new[] { tempFolder.GetRootPath() });

                var templateA = collection.GetTemplate("template-a");

                templateA.Should().NotBeNull();
                templateA.Name.ShouldBeEquivalentTo("template-a");
                templateA.RenderIntoString(new object()).ShouldBeEquivalentTo("Contents A");
            }
        }

        [Fact]
        public void Test_template_loading_from_subfolder()
        {
            using (var tempFolder = new TempFolder())
            {
                var collection = new TemplateCollection();

                tempFolder.CreateWriteTextFile("folder/template-a.hbs", "Contents A");
                collection.LoadTemplates(new[] { tempFolder.GetRootPath() });

                var templateA = collection.GetTemplate("folder/template-a");

                templateA.Should().NotBeNull();
                templateA.Name.ShouldBeEquivalentTo("folder/template-a");
                templateA.RenderIntoString(new object()).ShouldBeEquivalentTo("Contents A");
            }
        }

        [Fact]
        public void Test_template_loading_with_override()
        {
            using (var tempFolder = new TempFolder())
            {
                var collection = new TemplateCollection();

                tempFolder.CreateWriteTextFile("folder1/template-a.hbs", "Contents A");
                tempFolder.CreateWriteTextFile("folder2/template-a.hbs", "Contents B");

                collection.LoadTemplates(new[] { tempFolder.GetPath("folder1"), tempFolder.GetPath("folder2") });

                var templateA = collection.GetTemplate("template-a");

                templateA.Should().NotBeNull();
                templateA.Name.ShouldBeEquivalentTo("template-a");
                templateA.RenderIntoString(new object()).ShouldBeEquivalentTo("Contents B");
            }
        }
    }
}
