using System;
using System.IO;
using Polygen.Core.Tests;
using FluentAssertions;
using Xunit;

namespace Polygen.App.Tests
{
    public class Solution1Test
    {
        [Fact]
        public void Generate_code_from_Solution1()
        {
            var testProjectDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));

            using (var tempFolder = new TempFolder())
            {
                tempFolder.CopyFrom(Path.Combine(testProjectDir, "Solution1"));

                var app = new Program();
                var exitCode = app.Execute(new[]
                {
                    "--config", Path.Combine(tempFolder.GetPath("DesignProject/ProjectConfiguration.xml"))
                });

                exitCode.ShouldBeEquivalentTo(0);

                var outputClassExists = File.Exists(tempFolder.GetPath("DataProject/Entity/TestApp/MyClasses/MyEntity.cs"));

                outputClassExists.Should().BeTrue();
            }
        }
    }
}
