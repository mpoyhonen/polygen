using Polygen.Core.Impl.DesignModel;
using Polygen.Plugins.Base.NamingConvention;
using FluentAssertions;
using Xunit;

namespace Polygen.Plugins.Base.Tests
{
    public class JavascriptClassNamingConventionTests
    {
        [Fact]
        public void Test_class_name()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetClassName("MyClass", false).Should().Be("MyClass");
        }

        [Fact]
        public void Test_interface_name()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetClassName("MyInterface", true).Should().Be("IMyInterface");
        }

        [Fact]
        public void Test_method_name()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetMethodName("MyMethod").Should().Be("myMethod");
        }

        [Fact]
        public void Test_property_name()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetPropertyName("MyProperty").Should().Be("myProperty");
        }

        [Fact]
        public void Test_namespace_name_one_part()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetNamespaceName(new Namespace("MyApp", null)).Should().Be("MyApp");
        }

        [Fact]
        public void Test_namespace_name_two_parts()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetNamespaceName(new Namespace("MyApp.MyModule", null)).Should().Be("MyApp.mymodule");
        }

        [Fact]
        public void Test_namespace_name_three_parts()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetNamespaceName(new Namespace("MyApp.Views.MyModule", null)).Should().Be("MyApp.views.mymodule");
        }

        [Fact]
        public void Test_output_folder_path()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetOutputFolderPath(new Namespace("MyApp.MyModule", null)).Should().Be("MyApp/mymodule");
        }
    }
}
