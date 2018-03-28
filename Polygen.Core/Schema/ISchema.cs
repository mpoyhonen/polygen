using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Polygen.Core.Schema;

namespace Polygen.Core.Schema
{
    /// <summary>
    /// Interface for a schema which contains all registered schema elements.
    /// </summary>
    public interface ISchema
    {
        /// <summary>
        /// Schema XML namespace.
        /// </summary>
        XNamespace Namespace { get; }
        /// <summary>
        /// Schema name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Root schema element.
        /// </summary>
        ISchemaElement RootElement { get; }
        /// <summary>
        /// Creates the schema root element.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ISchemaElementBuilder CreateRootElement(XName name);
    }
}
