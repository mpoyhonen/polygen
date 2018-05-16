using System.Linq;
using Xunit;
using FluentAssertions;
using System.Xml.Linq;
using Polygen.TestUtils.DataType;

namespace Polygen.Core.Tests
{
    public class SchemaTests
    {
        [Fact]
        public void Create_root_element()
        {
            var ns = (XNamespace)"urn:test";
            var schema = new Impl.Schema.Schema("schema", ns);

            schema
                .CreateRootElement(ns + "root")
                    .CreateAttribute(ns + "a", TestDataTypes.String);

            schema.RootElement.Should().NotBeNull();
            schema.RootElement.Name.Should().Be(ns + "root");
            schema.RootElement.Attributes.Select(x => x.Name).Should().BeEquivalentTo(new[] { ns + "a" });
        }

        [Fact]
        public void Create_root_element_with_child_element()
        {
            var ns = (XNamespace)"urn:test";
            var schema = new Impl.Schema.Schema("schema", ns);

            schema
                .CreateRootElement(ns + "root")
                    .CreateAttribute(ns + "a", TestDataTypes.String)
                    .CreateAttribute(ns + "b", TestDataTypes.String)
                    .CreateElement(ns + "child")
                        .CreateAttribute(ns + "a", TestDataTypes.String);

            schema.RootElement.Children.Count.Should().Be(1);
            schema.RootElement.Children[0].Name.Should().Be(ns + "child");
        }
    }
}
