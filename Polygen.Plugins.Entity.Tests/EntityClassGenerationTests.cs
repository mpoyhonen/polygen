using Polygen.Core.Tests;
using FluentAssertions;
using System.IO;
using Xunit;

namespace Polygen.Plugins.Entity.Tests
{
    public class EntityClassGenerationTests
    {
        [Fact]
        public void Test_generated_entity_classes()
        {
            using (var tempFolder = new TempFolder())
            {
                const string projectConfigurationFile = "solution/DesignProject/ProjectConfiguration.xml";

                tempFolder.CreateWriteTextFile(projectConfigurationFile,
@"
<ProjectConfiguration xmlns='uri:polygen/1.0/project-configuration'>
    <Solution path='..'>
        <Project name='DesignProject' path='DesignProject' type='Design' />
        <Project name='DataProject' path='DataProject' type='Data' />
    </Solution>
</ProjectConfiguration>
");

                const string designModelsFile = "solution/DesignProject/DesignModels/DesignModels.xml";

                tempFolder.CreateWriteTextFile(designModelsFile,
@"
<DesignModels xmlns='uri:polygen/1.0/designmodel'>
    <Namespace name='TestApp.MyClasses'>
        <Entity name='MyEntity'>
            <Attribute name='Name' type='string' />
        </Entity>
    </Namespace>
</DesignModels>
");

                var outputFolder = tempFolder.CreateFolder("output");
                var runner = TestRunner.Create(new Core.AutofacModule(), new Base.AutofacModule(), new Templates.HandlebarsNet.AutofacModule(), new AutofacModule());

                runner.Execute(new Core.RunnerConfiguration
                {
                    ProjectConfigurationFile = tempFolder.GetPath(projectConfigurationFile),
                    TempFolder = outputFolder
                });

                var generatedEntityClassFile = tempFolder.GetPath("output/DataProject/Entity/TestApp/MyClasses/MyEntity.gen.cs");
                var customEntityClassFile = tempFolder.GetPath("output/DataProject/Entity/TestApp/MyClasses/MyEntity.cs");

                File.Exists(generatedEntityClassFile).Should().BeTrue();

                var generatedEntityClass = File.ReadAllText(generatedEntityClassFile).Trim();

                generatedEntityClass.Should().Contain("public partial class MyEntity");
                generatedEntityClass.Should().Contain("namespace TestApp.MyClasses");
                generatedEntityClass.Should().Contain("public string Name { get; set; }");
                generatedEntityClass.Should().Contain("using System;");

                var customEntityClass = File.ReadAllText(customEntityClassFile).Trim();

                customEntityClass.Should().Contain("public partial class MyEntity");
                customEntityClass.Should().Contain("namespace TestApp.MyClasses");
                customEntityClass.Should().NotContain("public string Name { get; set; }");
                customEntityClass.Should().Contain("using System;");
            }
        }
    }
}
