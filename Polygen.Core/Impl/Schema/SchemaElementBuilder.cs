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
            this._schemaElement = schemaElement;
        }

        public ISchemaElementBuilder CreateElement(XName name, string description, Action<ISchemaElement> configurator = null)
        {
            var child = new SchemaElement(this._schemaElement.Schema, name, description, this._schemaElement);

            configurator?.Invoke(child);
            this._schemaElement.AddChildElement(child);

            return new SchemaElementBuilder(child);
        }

        public ISchemaElementBuilder CreateElementWithValue(XName name, IDataType dataType, string description, Action<ISchemaElement> configurator = null)
        {
            var child = new SchemaElement(this._schemaElement.Schema, name, description, this._schemaElement)
            {
                UseContentAsValue = true,
                ValueType = dataType
            };

            configurator?.Invoke(child);
            this._schemaElement.AddChildElement(child);

            return new SchemaElementBuilder(child);
        }

        public ISchemaElementBuilder CreateAttribute(XName name, IDataType dataType, string description, Action<ISchemaElementAttribute> configurator = null)
        {
            var attribute = new SchemaElementAttribute(this._schemaElement, name, dataType, description);

            configurator?.Invoke(attribute);
            this._schemaElement.AddAttribute(attribute);

            return this;
        }

        public ISchemaElementBuilder Parent()
        {
            if (this._schemaElement.Parent == null)
            {
                throw new SchemaException("Parent element not set.");
            }

            return new SchemaElementBuilder(this._schemaElement.Parent);
        }

        public ISchemaElement Element()
        {
            return this._schemaElement;
        }
    }
}
