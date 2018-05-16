using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using HandlebarsDotNet;
using Xunit;

namespace Polygen.Templates.HandlebarsNet.Tests
{
    public class TemplateTests
    {
        [Fact]
        public void Test_template_registration()
        {
            var collection = new TemplateCollection();

            collection.AddTemplate("test", "Hi {{User.Name}}");

            var template = collection.GetTemplate("test");

            template.Should().NotBeNull();

            var data = new Dictionary<string, object>() {
                { "User", new { Name = "Test123" } }
            };

            using (var writer = new StringWriter())
            {
                template.Render(data, writer);

                writer.ToString().Should().Be("Hi Test123");
            }
        }

        [Fact]
        public void Test_template_registration_with_partial_include()
        {
            var collection = new TemplateCollection();

            collection.AddTemplate("test", "Hi {{User.Name}}, {{> include/partial}}");
            collection.AddTemplate("include/partial", "Included for {{User.Name}}");

            var template = collection.GetTemplate("test");
            var data = new Dictionary<string, object>() {
                { "User", new { Name = "Test123" } }
            };

            using (var writer = new StringWriter())
            {
                template.Render(data, writer);

                writer.ToString().Should().Be("Hi Test123, Included for Test123");
            }
        }

        [Fact]
        public void Test_template_rendering()
        {
            var instance = Handlebars.Create(new HandlebarsConfiguration
            {
                TextEncoder = new NoopTextEncoder()
            });
            var template = new Template("test", instance, TemplateSource.CreateForText("Hello {{name}}"));

            template.Name.Should().Be("test");
            template.RenderIntoString(new { name = "Test123" }).Should().Be("Hello Test123");
        }

        [Fact]
        public void Test_template_rendering_with_escaped_characters()
        {
            var instance = Handlebars.Create(new HandlebarsConfiguration
            {
                TextEncoder = new NoopTextEncoder()
            });
            var template = new Template("test", instance, TemplateSource.CreateForText("|{{name}}|"));

            template.Name.Should().Be("test");
            template.RenderIntoString(new { name = "< > / \\ & $ # @" }).Should().Be("|< > / \\ & $ # @|");
        }
    }
}
