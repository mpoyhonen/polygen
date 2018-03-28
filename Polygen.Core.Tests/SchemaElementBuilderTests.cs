using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Polygen.Core.Impl.DataType;
using Polygen.Core.Impl.Schema;
using Polygen.Core.Schema;
using Polygen.TestUtils.DataType;
using FluentAssertions;
using Xunit;

namespace Polygen.Core.Tests
{
    public class SchemaElementBuilderTests
    {
        private static readonly XNamespace NS1 = "urn:test";

        [Fact]
        public void Create_root_element()
        {
            var schema = new Impl.Schema.Schema("schema", NS1);
            var element = schema
                .CreateRootElement(NS1 + "root-element")
                    .CreateAttribute(NS1 + "attr", TestDataTypes.String)
                    .Element();

            element.Name.ShouldBeEquivalentTo(NS1 + "root-element");
            element.Attributes.Select(x => x.Name).ShouldBeEquivalentTo(new[] { NS1 + "attr" });
        }

        [Fact]
        public void Create_child_element()
        {
            var schema = new Impl.Schema.Schema("schema", NS1);
            var element = schema
                .CreateRootElement(NS1 + "root-element")
                    .CreateElement(NS1 + "child-element")
                        .CreateAttribute(NS1 + "attr", TestDataTypes.String)
                        .Element();

            element.Name.ShouldBeEquivalentTo(NS1 + "child-element");
            element.Attributes.Select(x => (x.Name, x.Type?.Name)).ShouldBeEquivalentTo(new[] { (NS1 + "attr", TestDataTypes.String.Name) });
            element.Parent.Name.ShouldBeEquivalentTo(NS1 + "root-element");
        }

        [Fact]
        public void Create_child_element_with_options()
        {
            var schema = new Impl.Schema.Schema("schema", NS1);
            var element = schema
                .CreateRootElement(NS1 + "root-element")
                    .CreateElement(NS1 + "child-element", null, x =>
                    {
                        x.IsMandatory = true;
                        x.AllowMultiple = true;
                        x.UseContentAsValue = true;
                    })
                    .Element();

            element.Name.ShouldBeEquivalentTo(NS1 + "child-element");
            element.IsMandatory.Should().BeTrue();
            element.AllowMultiple.Should().BeTrue();
            element.UseContentAsValue.Should().BeTrue();
        }

        [Fact]
        public void Create_child_element_with_attribute_options()
        {
            var schema = new Impl.Schema.Schema("schema", NS1);
            var element = schema
                .CreateRootElement(NS1 + "root-element")
                    .CreateElement(NS1 + "child-element")
                        .CreateAttribute(NS1 + "attr", TestDataTypes.Int, null, x =>
                        {
                            x.DefaultValue = "123";
                            x.IsMandatory = true;
                        })
                        .Element();

            var attribute = element.Attributes[0];

            attribute.Name.ShouldBeEquivalentTo(NS1 + "attr");
            attribute.DefaultValue.ShouldBeEquivalentTo("123");
            attribute.IsMandatory.Should().BeTrue();
        }
    }
}
