using System.Xml.Linq;
using Polygen.Core.Exceptions;
using Polygen.Core.Schema;

namespace Polygen.Core.Impl.Schema
{
    public class Schema : ISchema
    {
        public Schema(string name, XNamespace schemaNamespace)
        {
            Name = name;
            Namespace = schemaNamespace;
        }

        public string Name { get; }
        public XNamespace Namespace { get; }
        public ISchemaElement RootElement { get; private set; }

        public void SetRootElement(ISchemaElement element)
        {
            RootElement = element;
        }

        public ISchemaElementBuilder CreateRootElement(XName name)
        {
            if (RootElement != null)
            {
                throw new SchemaException("Root element is already set.");
            }

            RootElement = new SchemaElement(this, name, null);

            return new SchemaElementBuilder(RootElement);
        }
    }
}
