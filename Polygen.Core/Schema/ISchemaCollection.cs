using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Polygen.Core.Schema
{
    /// <summary>
    /// Contains all defined schemas.
    /// </summary>
    public interface ISchemaCollection
    {
        /// <summary>
        /// Contains all defines schemas.
        /// </summary>
        IReadOnlyList<ISchema> Schemas { get; }
        /// <summary>
        /// Adds a new schema.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="schemaNamespace"></param>
        /// <returns></returns>
        ISchema AddSchema(string name, XNamespace schemaNamespace);
        /// <summary>
        /// Returns a schema by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ISchema GetSchemaByName(string name, bool throwIfMissing = true);
        /// <summary>
        /// Returns a schema by XML namespace.
        /// </summary>
        /// <param name="schemaNamespace"></param>
        /// <returns></returns>
        ISchema GetSchemaByNamespace(XNamespace schemaNamespace, bool throwIfMissing = true);
    }
}
