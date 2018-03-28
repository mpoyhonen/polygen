using Polygen.Core.Impl.Project;
using Polygen.Core.Utils;
using Polygen.TestUtils.NamingConvention;
using FluentAssertions;
using Xunit;

namespace Polygen.Core.Tests.OutputConfiguration
{
    public class OutputConfigurationTests
    {

        [Fact]
        public void Test_output_folder_from_root_with_simple_name()
        {
            var rootConfig = new Impl.OutputConfiguration.OutputConfiguration(null);
            var outputFolder = new ProjectFolder("test");

            rootConfig.RegisterOutputFolder(new Filter("Entity/TestClass"), outputFolder);
            rootConfig.GetOutputFolder("Entity/TestClass").Should().BeSameAs(outputFolder);
        }

        [Fact]
        public void Test_output_folder_from_root_with_glob_name()
        {
            var rootConfig = new Impl.OutputConfiguration.OutputConfiguration(null);
            var outputFolder = new ProjectFolder("test");

            rootConfig.RegisterOutputFolder(new Filter("Entity/*"), outputFolder);
            rootConfig.GetOutputFolder("Entity/TestClass").Should().BeSameAs(outputFolder);
        }

        [Fact]
        public void Test_output_folder_from_root_with_glob_path()
        {
            var rootConfig = new Impl.OutputConfiguration.OutputConfiguration(null);
            var outputFolder = new ProjectFolder("test");

            rootConfig.RegisterOutputFolder(new Filter("**/TestClass"), outputFolder);
            rootConfig.GetOutputFolder("Entity/TestClass").Should().BeSameAs(outputFolder);
        }

        [Fact]
        public void Test_output_folder_from_child_with_simple_name()
        {
            var rootConfig = new Impl.OutputConfiguration.OutputConfiguration(null);
            var childConfig = new Impl.OutputConfiguration.OutputConfiguration(rootConfig);
            var outputFolder = new ProjectFolder("test");

            rootConfig.RegisterOutputFolder(new Filter("Entity/TestClass"), outputFolder);
            childConfig.GetOutputFolder("Entity/TestClass").Should().BeSameAs(outputFolder);
        }

        [Fact]
        public void Test_output_folder_from_child_with_glob_name()
        {
            var rootConfig = new Impl.OutputConfiguration.OutputConfiguration(null);
            var childConfig = new Impl.OutputConfiguration.OutputConfiguration(rootConfig);
            var outputFolder = new ProjectFolder("test");

            rootConfig.RegisterOutputFolder(new Filter("Entity/*"), outputFolder);
            childConfig.GetOutputFolder("Entity/TestClass").Should().BeSameAs(outputFolder);
        }
    }
}
