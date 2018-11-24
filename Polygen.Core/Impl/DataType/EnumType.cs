using System.Linq;
using System.Xml.Linq;
using Polygen.Core.DataType;

namespace Polygen.Core.Impl.DataType
{
	/// <inheritdoc />
	/// <summary>
	/// Data type implementation for an enumeration type.
	/// </summary>
	public class EnumType : IDataType
	{
		public EnumType(string name, params Value[] values)
		{
			Name = name;
			XsdName = "tns:enum-" + name;
            Values = values;
		}

		public string Name { get; }
		public string XsdName { get; }
        public Value[] Values { get; }

		public void PostProcessXsdDefinition(XElement schemaRootElement)
		{
            var ns = schemaRootElement.Name.Namespace;
            var valueElements = Values
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

        public class Value
        {
            public Value(string name, string description = null)
            {
                Name = name;
                Description = description;
            }

            public string Name { get; set; }
            public string Description { get; set; }
        }
	}
}
