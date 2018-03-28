using Polygen.Core.Tests;
using FluentAssertions;
using System.IO;
using Xunit;
using Polygen.Core.Impl.Schema;
using Polygen.Plugins.Base.Output.Xsd;
using System.Xml.Linq;
using Polygen.TestUtils;
using System;
using Polygen.Core.Impl.DataType;

namespace Polygen.Plugins.Base.Tests
{
    public class SchemaConverterTests
    {
        private static readonly XNamespace XsdNamespace= "http://www.w3.org/2001/XMLSchema";

        [Fact]
        public void Test_empty_element()
        {
            var xsd = XsdNamespace;
            var ns = "uri:test-schema";
            var schema = new Schema("test", ns);

            schema
                .CreateRootElement("DesignModels")
                    .CreateElement("Element");

            var outputXsd = new SchemaConverter().Convert(schema);

            outputXsd.Should().NotBeNull();

            var outputXsdStr = outputXsd.Element.ToTestString().Trim();

            outputXsdStr.ShouldBeEquivalentTo(@"
<xs:schema elementFormDefault='qualified' targetNamespace='uri:test-schema' xmlns:tns='uri:test-schema' xmlns:xs='http://www.w3.org/2001/XMLSchema'>
    <xs:element name='DesignModels'>
        <xs:complexType>
            <xs:sequence>
                <xs:element maxOccurs='1' minOccurs='0' name='Element'/>
            </xs:sequence>
        </xs:complexType>
    </xs:element>
</xs:schema>
".Trim());
        }

        [Fact]
        public void Test_empty_mandatory_element()
        {
            var xsd = XsdNamespace;
            var ns = "uri:test-schema";
            var schema = new Schema("test", ns);

            schema
                .CreateRootElement("DesignModels")
                    .CreateElement("Element", null, c => c.IsMandatory = true);

            var outputXsd = new SchemaConverter().Convert(schema);
            var outputXsdStr = outputXsd.Element.ToTestString().Trim();

            outputXsdStr.ShouldBeEquivalentTo(@"
<xs:schema elementFormDefault='qualified' targetNamespace='uri:test-schema' xmlns:tns='uri:test-schema' xmlns:xs='http://www.w3.org/2001/XMLSchema'>
    <xs:element name='DesignModels'>
        <xs:complexType>
            <xs:sequence>
                <xs:element maxOccurs='1' minOccurs='1' name='Element'/>
            </xs:sequence>
        </xs:complexType>
    </xs:element>
</xs:schema>
".Trim());
        }

        [Fact]
        public void Test_empty_repeating_element()
        {
            var xsd = XsdNamespace;
            var ns = "uri:test-schema";
            var schema = new Schema("test", ns);

            schema
                .CreateRootElement("DesignModels")
                    .CreateElement("Element", null, c => c.AllowMultiple = true);

            var outputXsd = new SchemaConverter().Convert(schema);
            var outputXsdStr = outputXsd.Element.ToTestString().Trim();

            outputXsdStr.ShouldBeEquivalentTo(@"
<xs:schema elementFormDefault='qualified' targetNamespace='uri:test-schema' xmlns:tns='uri:test-schema' xmlns:xs='http://www.w3.org/2001/XMLSchema'>
    <xs:element name='DesignModels'>
        <xs:complexType>
            <xs:sequence>
                <xs:element maxOccurs='unbounded' minOccurs='0' name='Element'/>
            </xs:sequence>
        </xs:complexType>
    </xs:element>
</xs:schema>
".Trim());
        }


        [Fact]
        public void Test_empty_element_with_an_annotation()
        {
            var xsd = XsdNamespace;
            var ns = "uri:test-schema";
            var schema = new Schema("test", ns);

            schema
                .CreateRootElement("DesignModels")
                    .CreateElement("Element", "Empty element");

            var outputXsd = new SchemaConverter().Convert(schema);
            var outputXsdStr = outputXsd.Element.ToTestString().Trim();

            outputXsdStr.ShouldBeEquivalentTo(@"
<xs:schema elementFormDefault='qualified' targetNamespace='uri:test-schema' xmlns:tns='uri:test-schema' xmlns:xs='http://www.w3.org/2001/XMLSchema'>
    <xs:element name='DesignModels'>
        <xs:complexType>
            <xs:sequence>
                <xs:element maxOccurs='1' minOccurs='0' name='Element'>
                    <xs:annotation>
                        <xs:documentation>
                            Empty element
                        </xs:documentation>
                    </xs:annotation>
                </xs:element>
            </xs:sequence>
        </xs:complexType>
    </xs:element>
</xs:schema>
".Trim());
        }

        [Fact]
        public void Test_element_with_an_attribute()
        {
            var xsd = XsdNamespace;
            var ns = "uri:test-schema";
            var schema = new Schema("test", ns);
            var stringType = new PrimitiveType("string", "xs:string");

            schema
                .CreateRootElement("DesignModels")
                    .CreateElement("Element")
                        .CreateAttribute("attrib", stringType);

            var outputXsd = new SchemaConverter().Convert(schema);
            var outputXsdStr = outputXsd.Element.ToTestString().Trim();

            outputXsdStr.ShouldBeEquivalentTo(@"
<xs:schema elementFormDefault='qualified' targetNamespace='uri:test-schema' xmlns:tns='uri:test-schema' xmlns:xs='http://www.w3.org/2001/XMLSchema'>
    <xs:element name='DesignModels'>
        <xs:complexType>
            <xs:sequence>
                <xs:element maxOccurs='1' minOccurs='0' name='Element'>
                    <xs:complexType>
                        <xs:attribute name='attrib' type='xs:string'/>
                    </xs:complexType>
                </xs:element>
            </xs:sequence>
        </xs:complexType>
    </xs:element>
</xs:schema>
".Trim());
        }

        [Fact]
        public void Test_element_with_a_value()
        {
            var xsd = XsdNamespace;
            var ns = "uri:test-schema";
            var schema = new Schema("test", ns);
            var stringType = new PrimitiveType("string", "xs:string");

            schema
                .CreateRootElement("DesignModels")
                    .CreateElementWithValue("Element", stringType);

            var outputXsd = new SchemaConverter().Convert(schema);
            var outputXsdStr = outputXsd.Element.ToTestString().Trim();

            outputXsdStr.ShouldBeEquivalentTo(@"
<xs:schema elementFormDefault='qualified' targetNamespace='uri:test-schema' xmlns:tns='uri:test-schema' xmlns:xs='http://www.w3.org/2001/XMLSchema'>
    <xs:element name='DesignModels'>
        <xs:complexType>
            <xs:sequence>
                <xs:element maxOccurs='1' minOccurs='0' name='Element'>
                    <xs:complexType>
                        <xs:simpleContent>
                            <xs:extension base='xs:string'/>
                        </xs:simpleContent>
                    </xs:complexType>
                </xs:element>
            </xs:sequence>
        </xs:complexType>
    </xs:element>
</xs:schema>
".Trim());
        }

        [Fact]
        public void Test_element_with_a_value_and_an_attribute()
        {
            var xsd = XsdNamespace;
            var ns = "uri:test-schema";
            var schema = new Schema("test", ns);
            var stringType = new PrimitiveType("string", "xs:string");
            var intType = new PrimitiveType("int", "xs:int");

            schema
                .CreateRootElement("DesignModels")
                    .CreateElementWithValue("Element", stringType)
                        .CreateAttribute("attrib", intType);

            var outputXsd = new SchemaConverter().Convert(schema);
            var outputXsdStr = outputXsd.Element.ToTestString().Trim();

            outputXsdStr.ShouldBeEquivalentTo(@"
<xs:schema elementFormDefault='qualified' targetNamespace='uri:test-schema' xmlns:tns='uri:test-schema' xmlns:xs='http://www.w3.org/2001/XMLSchema'>
    <xs:element name='DesignModels'>
        <xs:complexType>
            <xs:sequence>
                <xs:element maxOccurs='1' minOccurs='0' name='Element'>
                    <xs:complexType>
                        <xs:simpleContent>
                            <xs:extension base='xs:string'>
                                <xs:attribute name='attrib' type='xs:int'/>
                            </xs:extension>
                        </xs:simpleContent>
                    </xs:complexType>
                </xs:element>
            </xs:sequence>
        </xs:complexType>
    </xs:element>
</xs:schema>
".Trim());
        }

        [Fact]
        public void Test_element_with_an_enum_value()
        {
            var xsd = XsdNamespace;
            var ns = "uri:test-schema";
            var schema = new Schema("test", ns);
            var enumType = new EnumType("testEnum", new EnumType.Value("one"), new EnumType.Value("two", "Second"));

            schema
                .CreateRootElement("DesignModels")
                    .CreateElementWithValue("Element", enumType);

            var outputXsd = new SchemaConverter().Convert(schema);
            var outputXsdStr = outputXsd.Element.ToTestString().Trim();

            outputXsdStr.ShouldBeEquivalentTo(@"
<xs:schema elementFormDefault='qualified' targetNamespace='uri:test-schema' xmlns:tns='uri:test-schema' xmlns:xs='http://www.w3.org/2001/XMLSchema'>
    <xs:element name='DesignModels'>
        <xs:complexType>
            <xs:sequence>
                <xs:element maxOccurs='1' minOccurs='0' name='Element'>
                    <xs:complexType>
                        <xs:simpleContent>
                            <xs:extension base='tns:enum-testEnum'/>
                        </xs:simpleContent>
                    </xs:complexType>
                </xs:element>
            </xs:sequence>
        </xs:complexType>
    </xs:element>
    <xs:simpleType name='enum-testEnum'>
        <xs:restriction base='xs:string'>
            <xs:enumeration value='one'/>
            <xs:enumeration value='two'>
                <xs:annotation>
                    <xs:documentation>
                        Second
                    </xs:documentation>
                </xs:annotation>
            </xs:enumeration>
        </xs:restriction>
    </xs:simpleType>
</xs:schema>
".Trim());
    }

        [Fact]
        public void Test_element_with_an_enum_attribute()
        {
            var xsd = XsdNamespace;
            var ns = "uri:test-schema";
            var schema = new Schema("test", ns);
            var enumType = new EnumType("testEnum", new EnumType.Value("one"), new EnumType.Value("two", "Second"));

            schema
                .CreateRootElement("DesignModels")
                    .CreateElement("Element")
                        .CreateAttribute("attrib", enumType);

            var outputXsd = new SchemaConverter().Convert(schema);
            var outputXsdStr = outputXsd.Element.ToTestString().Trim();

            outputXsdStr.ShouldBeEquivalentTo(@"
<xs:schema elementFormDefault='qualified' targetNamespace='uri:test-schema' xmlns:tns='uri:test-schema' xmlns:xs='http://www.w3.org/2001/XMLSchema'>
    <xs:element name='DesignModels'>
        <xs:complexType>
            <xs:sequence>
                <xs:element maxOccurs='1' minOccurs='0' name='Element'>
                    <xs:complexType>
                        <xs:attribute name='attrib' type='tns:enum-testEnum'/>
                    </xs:complexType>
                </xs:element>
            </xs:sequence>
        </xs:complexType>
    </xs:element>
    <xs:simpleType name='enum-testEnum'>
        <xs:restriction base='xs:string'>
            <xs:enumeration value='one'/>
            <xs:enumeration value='two'>
                <xs:annotation>
                    <xs:documentation>
                        Second
                    </xs:documentation>
                </xs:annotation>
            </xs:enumeration>
        </xs:restriction>
    </xs:simpleType>
</xs:schema>
".Trim());
        }
    }
}
