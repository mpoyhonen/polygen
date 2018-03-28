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

            namingConvention.GetClassName("MyClass", false).ShouldBeEquivalentTo("MyClass");
        }

        [Fact]
        public void Test_interface_name()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetClassName("MyInterface", true).ShouldBeEquivalentTo("IMyInterface");
        }

        [Fact]
        public void Test_method_name()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetMethodName("MyMethod").ShouldBeEquivalentTo("myMethod");
        }

        [Fact]
        public void Test_property_name()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetPropertyName("MyProperty").ShouldBeEquivalentTo("myProperty");
        }

        [Fact]
        public void Test_namespace_name_one_part()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetNamespaceName(new Namespace("MyApp", null)).ShouldBeEquivalentTo("MyApp");
        }

        [Fact]
        public void Test_namespace_name_two_parts()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetNamespaceName(new Namespace("MyApp.MyModule", null)).ShouldBeEquivalentTo("MyApp.mymodule");
        }

        [Fact]
        public void Test_namespace_name_three_parts()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetNamespaceName(new Namespace("MyApp.Views.MyModule", null)).ShouldBeEquivalentTo("MyApp.views.mymodule");
        }

        [Fact]
        public void Test_output_folder_path()
        {
            var namingConvention = new JavascriptClassNamingConvention();

            namingConvention.GetOutputFolderPath(new Namespace("MyApp.MyModule", null)).ShouldBeEquivalentTo("MyApp/mymodule");
        }
    }
}
