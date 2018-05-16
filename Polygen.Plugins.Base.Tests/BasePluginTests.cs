using System;
using System.Linq;
using Polygen.Core.Project;
using Polygen.Core.Tests;
using FluentAssertions;
using Xunit;

namespace Polygen.Plugins.Base.Tests
{
    public class BasePluginTests
    {
        [Fact]
        public void Test_project_configuration_file_parsing()
        {
            using (var tempFolder = new TempFolder())
            {
                const string designModelPath = "ProjectConfiguration.xml";

                tempFolder.CreateWriteTextFile(designModelPath,
@"
<ProjectConfiguration xmlns='uri:polygen/1.0/project-configuration'>
    <Solution path='solution'>
        <Project name='DesignProject' path='DesignProject' type='Design' />
        <Project name='WebProject' path='WebProject' type='Web' />
    </Solution>
</ProjectConfiguration>
");

                var runner = TestRunner.Create(new Core.AutofacModule(), new AutofacModule());

                runner.Initialize();
                runner.RegisterSchemas();
                runner.ParseProjectConfiguration(tempFolder.GetPath(designModelPath));

                var projectConfiguration = runner.Context.DesignModels.GetByType(Core.CoreConstants.DesignModelType_ProjectConfiguration).FirstOrDefault() as IProjectConfiguration;

                projectConfiguration.Should().NotBeNull();
                projectConfiguration.Projects.Projects.Count().Should().Be(2);

                var projects = projectConfiguration.Projects.Projects.Select(x => (name: x.Name, path: x.SourceFolder, type: x.Type));

                projects.Should().BeEquivalentTo(new[] {
                    (name: "DesignProject", path: tempFolder.GetPath("solution/DesignProject"), type: "Design"),
                    (name: "WebProject", path: tempFolder.GetPath("solution/WebProject"), type: "Web")
                });
            }
        }
    }
}
