using Polygen.Core.Tests;
using FluentAssertions;
using System.IO;
using Xunit;

namespace Polygen.Plugins.Base.Tests
{
    public class SchemaGenerationTests
    {
        [Fact]
        public void Test_generated_schemas()
        {
            using (var tempFolder = new TempFolder())
            {
                const string projectConfigurationFile = "ProjectConfiguration.xml";

                tempFolder.CreateWriteTextFile(projectConfigurationFile,
@"
<ProjectConfiguration xmlns='uri:polygen/1.0/project-configuration'>
    <Solution path='solution'>
        <Project name='DesignProject' path='DesignProject' type='Design' />
    </Solution>
</ProjectConfiguration>
");

                var outputFolder = tempFolder.CreateFolder("output");
                var runner = TestRunner.Create(new Core.AutofacModule(), new AutofacModule());

                runner.Execute(new Core.RunnerConfiguration
                {
                    ProjectConfigurationFile = tempFolder.GetPath(projectConfigurationFile),
                    TempFolder = outputFolder
                });

                var projectConfigurationSchemaFile = tempFolder.GetPath("output/DesignProject/Schemas/XSD/ProjectConfiguration.xsd");

                File.Exists(projectConfigurationSchemaFile).Should().BeTrue();

                var projectConfigurationSchema = File.ReadAllText(projectConfigurationSchemaFile).Trim();

                projectConfigurationSchema.Should().Contain("<xs:element name=\"Solution\"");

                var designModelSchemaFile = tempFolder.GetPath("output/DesignProject/Schemas/XSD/DesignModels.xsd");

                File.Exists(designModelSchemaFile).Should().BeTrue();

                var designModelSchema = File.ReadAllText(designModelSchemaFile).Trim();

                designModelSchema.Should().Contain("<xs:element name=\"Namespace\"");
            }
        }
    }
}
