using System.Xml.Linq;
using Polygen.Core.DataType;

namespace Polygen.Core.Impl.DataType
{
	/// <inheritdoc />
	/// <summary>
	/// Data type implementation for primitive types.
	/// </summary>
	public class PrimitiveType : IDataType
	{
		public PrimitiveType(string name, string xsdName)
		{
			Name = name;
			XsdName = xsdName;

		}
		public string Name { get; }
		public string XsdName { get; }

        public void PostProcessXsdDefinition(XElement schemaRootElement)
        {
        }
    }
}
