using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Polygen.Core.Exceptions;
using Polygen.Core.Schema;

namespace Polygen.Core.Impl.Schema
{
    public class SchemaCollection : ISchemaCollection
    {
        private List<ISchema> _schemas = new List<ISchema>();
        private Dictionary<string, ISchema> _schemaByNameMap = new Dictionary<string, ISchema>();
        private Dictionary<XNamespace, ISchema> _schemaByNamespaceMap = new Dictionary<XNamespace, ISchema>();

        public IReadOnlyList<ISchema> Schemas => this._schemas;

        public ISchema AddSchema(string name, XNamespace schemaNamespace)
        {
            if (this._schemaByNameMap.ContainsKey(name))
            {
                throw new SchemaException("Schema is already defined with name: " + name);
            }

            if (this._schemaByNamespaceMap.ContainsKey(schemaNamespace))
            {
                throw new SchemaException("Schema is already defined with XML namespace: " + schemaNamespace);
            }

            var schema = new Schema(name, schemaNamespace);

            this._schemaByNameMap.Add(name, schema);
            this._schemaByNamespaceMap.Add(schemaNamespace, schema);
            this._schemas.Add(schema);

            return schema;
        }

        public ISchema GetSchemaByName(string name, bool throwIfMissing = true)
        {
            if (!this._schemaByNameMap.TryGetValue(name, out var res) && throwIfMissing)
            {
                throw new SchemaException("Schema not found with name: " + name);
            }

            return res;
        }

        public ISchema GetSchemaByNamespace(XNamespace schemaNamespace, bool throwIfMissing = true)
        {
            if (!this._schemaByNamespaceMap.TryGetValue(schemaNamespace, out var res) && throwIfMissing)
            {
                throw new SchemaException("Schema not found with XML namespace: " + schemaNamespace);
            }

            return res;
        }
    }
}
