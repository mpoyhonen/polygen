using Polygen.Core.Impl.DesignModel;
using Polygen.Plugins.Base.NamingConvention;
using FluentAssertions;
using Xunit;

namespace Polygen.Plugins.Base.Tests
{
    public class CSharpClassNamingConventionTests
    {
        [Fact]
        public void Test_class_name()
        {
            var namingConvention = new CSharpClassNamingConvention();

            namingConvention.GetClassName("MyClass", false).ShouldBeEquivalentTo("MyClass");
        }

        [Fact]
        public void Test_interface_name()
        {
            var namingConvention = new CSharpClassNamingConvention();

            namingConvention.GetClassName("MyInterface", true).ShouldBeEquivalentTo("IMyInterface");
        }

        [Fact]
        public void Test_method_name()
        {
            var namingConvention = new CSharpClassNamingConvention();

            namingConvention.GetMethodName("MyMethod").ShouldBeEquivalentTo("MyMethod");
        }

        [Fact]
        public void Test_property_name()
        {
            var namingConvention = new CSharpClassNamingConvention();

            namingConvention.GetPropertyName("MyProperty").ShouldBeEquivalentTo("MyProperty");
        }

        [Fact]
        public void Test_namespace_name()
        {
            var namingConvention = new CSharpClassNamingConvention();

            namingConvention.GetNamespaceName(new Namespace("MyApp.MyModule", null)).ShouldBeEquivalentTo("MyApp.MyModule");
        }

        [Fact]
        public void Test_output_folder_path()
        {
            var namingConvention = new CSharpClassNamingConvention();

            namingConvention.GetOutputFolderPath(new Namespace("MyApp.MyModule", null)).ShouldBeEquivalentTo("MyApp/MyModule");
        }
    }
}
