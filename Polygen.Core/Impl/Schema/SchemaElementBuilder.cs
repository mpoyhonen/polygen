using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Polygen.Core.DataType;
using Polygen.Core.Exceptions;
using Polygen.Core.Schema;

namespace Polygen.Core.Impl.Schema
{
    public class SchemaElementBuilder : ISchemaElementBuilder
    {
        private readonly ISchemaElement _schemaElement;

        public SchemaElementBuilder(ISchemaElement schemaElement)
        {
            _schemaElement = schemaElement;
        }

        public ISchemaElementBuilder CreateElement(XName name, string description, Action<ISchemaElement> configurator = null)
        {
            var child = new SchemaElement(_schemaElement.Schema, name, description, _schemaElement);

            configurator?.Invoke(child);
            _schemaElement.AddChildElement(child);

            return new SchemaElementBuilder(child);
        }

        public ISchemaElementBuilder CreateElementWithValue(XName name, IDataType dataType, string description, Action<ISchemaElement> configurator = null)
        {
            var child = new SchemaElement(_schemaElement.Schema, name, description, _schemaElement)
            {
                UseContentAsValue = true,
                ValueType = dataType
            };

            configurator?.Invoke(child);
            _schemaElement.AddChildElement(child);

            return new SchemaElementBuilder(child);
        }

        public ISchemaElementBuilder CreateAttribute(XName name, IDataType dataType, string description, Action<ISchemaElementAttribute> configurator = null)
        {
            var attribute = new SchemaElementAttribute(_schemaElement, name, dataType, description);

            configurator?.Invoke(attribute);
            _schemaElement.AddAttribute(attribute);

            return this;
        }

        public ISchemaElementBuilder Parent()
        {
            if (_schemaElement.Parent == null)
            {
                throw new SchemaException("Parent element not set.");
            }

            return new SchemaElementBuilder(_schemaElement.Parent);
        }

        public ISchemaElement Element()
        {
            return _schemaElement;
        }
    }
}
