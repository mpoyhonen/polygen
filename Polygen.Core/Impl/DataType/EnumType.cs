using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Polygen.Core.DataType;

namespace Polygen.Core.Impl.DataType
{
	/// <inheritdoc />
	/// <summary>
	/// Data type implementation for an enumeration type.
	/// </summary>
	public class EnumType : IEnumDataType
	{
	    /// <summary>
	    /// Data type prefix used in XSD.
	    /// </summary>
	    private const string XsdTypePrefix = "enum-";
	    /// <summary>
	    /// Contains the enumeration values.
	    /// </summary>
	    private readonly List<EnumDataTypeValue> _values;
	    
		public EnumType(string name, string description, params EnumDataTypeValue[] values)
		{
			Name = name;
		    Description = description;
		    XsdName = "tns:enum-" + name;
            _values = values.ToList();
		}

		public string Name { get; }
		public string XsdName { get; }
	    public string Description { get; }
	    public IEnumerable<EnumDataTypeValue> Values => _values;

		public void PostProcessXsdDefinition(XElement schemaRootElement)
		{
            var ns = schemaRootElement.Name.Namespace;
            var valueElements = _values
                .Select(x =>
                {
                    var enumValueElement = new XElement(ns + "enumeration", new XAttribute("value", x.Name));

                    if (!string.IsNullOrWhiteSpace(x.Description))
                    {
                        enumValueElement.Add(
                            new XElement(ns + "annotation",
                                new XElement(ns + "documentation", x.Description)
                            )
                        );
                    }

                    return enumValueElement;
                });

            var enumSimpleTypeElement = new XElement(ns + "simpleType",
                new XAttribute("name", "enum-" + Name),
                new XElement(ns + "restriction",
                    new XAttribute("base", "xs:string"),
                    valueElements
                )
            );

            schemaRootElement.Add(enumSimpleTypeElement);
		}
	}
}
