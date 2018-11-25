using Polygen.Core.Impl.Project;
using Polygen.Core.TargetPlatform;
using Polygen.Core.Utils;
using Polygen.TestUtils.NamingConvention;
using FluentAssertions;
using Xunit;

namespace Polygen.Core.Tests.OutputConfiguration
{
    public class TargetPlatformTests
    {
        [Fact]
        public void Test_register_naming_convention()
        {
            var targetPlatform = new Impl.TargetPlatform.TargetPlatform("x") as ITargetPlatform;
            var namingConvention = new TestClassNamingConvention();

            targetPlatform.RegisterNamingConvention("test", namingConvention);
            targetPlatform.GetNamingConvention("test").Should().BeSameAs(namingConvention);
        }
        
        [Fact]
        public void Test_register_naming_convention_with_overwrite()
        {
            var targetPlatform = new Impl.TargetPlatform.TargetPlatform("x") as ITargetPlatform;
            var namingConvention1 = new TestClassNamingConvention();
            var namingConvention2 = new TestClassNamingConvention();

            targetPlatform.RegisterNamingConvention("test", namingConvention1);
            targetPlatform.RegisterNamingConvention("test", namingConvention2, overwrite: true);
            targetPlatform.GetNamingConvention("test").Should().BeSameAs(namingConvention2);
        }
    }
}
