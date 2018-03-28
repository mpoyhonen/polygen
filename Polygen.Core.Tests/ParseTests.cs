using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Polygen.Core.Impl;
using Polygen.Core.Schema;
using Xunit;
using FluentAssertions;
using Polygen.Core.Impl.Schema;
using System.Xml.Linq;
using System.IO;
using Polygen.Core.Exceptions;
using System.Xml;
using Polygen.Core.Impl.Project;
using Polygen.TestUtils.DataType;

namespace Polygen.Core.Tests
{
    public class ParseTests
    {
        private static readonly XNamespace NS1 = "urn:test-ns1";
        private static readonly XNamespace NS2 = "urn:test-ns2";

        [Theory]
        [InlineData(@"")]
        [InlineData(@"<?xml version='1.0' encoding='UTF-8' standalone='no' ?>")]
        public void No_root_element_throws_an_exception(string xml)
        {
            var schemas = this.CreateSchemas();
            var schema = schemas.GetSchemaByNamespace(NS1);
            var parser = new Impl.Parser.XmlElementParser();
            var exception = Assert.Throws<XmlException>(() => parser.Parse(new StringReader(xml), schema, new ProjectFile("file.xml")));

            exception.Message.Should().Contain("Root element is missing");
        }

        [Theory]
        [InlineData(@"<root xmlns='urn:test-ns2' />")]
        [InlineData(@"<root2 xmlns='urn:test-ns1' />")]
        public void Invalid_root_element_throws_an_exception(string xml)
        {
            var schemas = this.CreateSchemas();
            var schema = schemas.GetSchemaByNamespace(NS1);
            var parser = new Impl.Parser.XmlElementParser();
            var exception = Assert.Throws<ParseException>(() => parser.Parse(new StringReader(xml), schema, new ProjectFile("file.xml")));

            exception.Message.Should().Contain("Invalid root XML element");
        }

        [Fact]
        public void Parse_root_element()
        {
            var schemas = this.CreateSchemas();
            var schema = schemas.GetSchemaByNamespace(NS1);
            var parser = new Impl.Parser.XmlElementParser();
            var xml = $@"
                <root xmlns='{NS1}' a='va' />
            ";

            var root = parser.Parse(new StringReader(xml), schema, new ProjectFile("file.xml"));

            root.Should().NotBeNull();
            root.Definition.Name.ShouldBeEquivalentTo(NS1 + "root");

            var attributes = root.Attributes.Select(x => (x.Definition.Name, x.Value));

            attributes.ShouldBeEquivalentTo(new[] { (NS1 + "a", "va") });
        }

        [Fact]
        public void Parse_one_child_element()
        {
            var schemas = this.CreateSchemas();
            var schema = schemas.GetSchemaByNamespace(NS1);
            var parser = new Impl.Parser.XmlElementParser();
            var xml = $@"
                <root xmlns='{NS1}' a='va'>
                    <child c='vc' d='vd' />
                </root>
            ";

            var root = parser.Parse(new StringReader(xml), schema, new ProjectFile("file.xml"));
            var child = root?.Children?.FirstOrDefault();

            child.Should().NotBeNull();
            child.Definition.Name.ShouldBeEquivalentTo(NS1 + "child");

            var attributes = child.Attributes.Select(x => (x.Definition.Name, x.Value));

            attributes.ShouldBeEquivalentTo(new[] { (NS1 + "c", "vc"), (NS1 + "d", "vd") });
        }

        [Fact]
        public void Parse_one_child_element_and_attribute_from_another_namespace()
        {
            var schemas = this.CreateSchemas();
            var schema = schemas.GetSchemaByNamespace(NS1);
            var parser = new Impl.Parser.XmlElementParser();
            var xml = $@"
                <root xmlns='{NS1}' xmlns:ns2='{NS2}' a='va'>
                    <child c='vc' ns2:e='ve' />
                </root>
            ";

            var root = parser.Parse(new StringReader(xml), schema, new ProjectFile("file.xml"));
            var child = root?.Children?.FirstOrDefault();

            child.Should().NotBeNull();
            child.Definition.Name.ShouldBeEquivalentTo(NS1 + "child");

            var attributes = child.Attributes.Select(x => (x.Definition.Name, x.Value));

            attributes.ShouldBeEquivalentTo(new[] { (NS1 + "c", "vc"), (NS2 + "e", "ve") });
        }

        [Fact]
        public void Parse_child_element_from_another_namespace()
        {
            var schemas = this.CreateSchemas();
            var schema = schemas.GetSchemaByNamespace(NS1);
            var parser = new Impl.Parser.XmlElementParser();
            var xml = $@"
                <root xmlns='{NS1}' xmlns:ns2='{NS2}' a='va'>
                    <ns2:child ns2:x='vx' />
                </root>
            ";

            var root = parser.Parse(new StringReader(xml), schema, new ProjectFile("file.xml"));
            var child = root?.Children?.FirstOrDefault();

            child.Should().NotBeNull();
            root.Definition.Name.ShouldBeEquivalentTo(NS1 + "root");
            child.Definition.Name.ShouldBeEquivalentTo(NS2 + "child");

            var attributes = child.Attributes.Select(x => (x.Definition.Name, x.Value));

            attributes.ShouldBeEquivalentTo(new[] { (NS2 + "x", "vx") });
        }

        [Fact]
        public void Parse_child_element_with_content_as_value()
        {
            var schemas = this.CreateSchemas();
            var schema = schemas.GetSchemaByNamespace(NS1);
            var parser = new Impl.Parser.XmlElementParser();
            var xml = $@"
                <root xmlns='{NS1}' a='va'>
                    <value f='vf'>
                        VALUE
                    </value>
                </root>
            ";

            var root = parser.Parse(new StringReader(xml), schema, new ProjectFile("file.xml"));
            var value = root?.Children?.FirstOrDefault();

            value.Should().NotBeNull();
            value.Definition.Name.ShouldBeEquivalentTo(NS1 + "value");

            var attributes = value.Attributes.Select(x => (x.Definition.Name, x.Value));

            attributes.ShouldBeEquivalentTo(new[] { (NS1 + "f", "vf") });
            value.Value.ShouldBeEquivalentTo("VALUE");
        }

        private ISchemaCollection CreateSchemas()
        {
            var schemaCollection = new Impl.Schema.SchemaCollection();
            var schema1 = schemaCollection.AddSchema("schema1", NS1.NamespaceName);

            schema1
                .CreateRootElement(NS1 + "root")
                    .CreateAttribute(NS1 + "a", TestDataTypes.String, "a - attribute")
                    .CreateAttribute(NS1 + "b", TestDataTypes.String, "b - attribute")
                    .CreateElement(NS1 + "child", "child - element")
                        .CreateAttribute(NS1 + "c", TestDataTypes.String, "c - attribute")
                        .CreateAttribute(NS1 + "d", TestDataTypes.String, "d - attribute")
                        .CreateAttribute(NS2 + "e", TestDataTypes.String, "e - attribute")
                        .Parent()
                    .CreateElement(NS2 + "child", "child - element (ns2)")
                        .CreateAttribute(NS2 + "x", TestDataTypes.String, "x - attribute")
                        .Parent()
                    .CreateElement(NS1 + "value", "value - element", x => x.UseContentAsValue = true)
                        .CreateAttribute(NS1 + "f", TestDataTypes.String, "f - attribute");

            return schemaCollection;
        }
    }
}
