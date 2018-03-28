using System.Xml.Linq;
using Polygen.Core.Exceptions;
using Polygen.Core.Schema;

namespace Polygen.Core.Impl.Schema
{
    public class Schema : ISchema
    {
        public Schema(string name, XNamespace schemaNamespace)
        {
            this.Name = name;
            this.Namespace = schemaNamespace;
        }

        public string Name { get; }
        public XNamespace Namespace { get; }
        public ISchemaElement RootElement { get; private set; }

        public void SetRootElement(ISchemaElement element)
        {
            this.RootElement = element;
        }

        public ISchemaElementBuilder CreateRootElement(XName name)
        {
            if (this.RootElement != null)
            {
                throw new SchemaException("Root element is already set.");
            }

            this.RootElement = new SchemaElement(this, name, null);

            return new SchemaElementBuilder(this.RootElement);
        }
    }
}
