using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Polygen.Core.DataType;

namespace Polygen.Core.Schema
{
    /// <summary>
    /// Builder interface for schema elements.
    /// </summary>
    public interface ISchemaElementBuilder
    {
        /// <summary>
        /// Creates a new child element.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="configurator"></param>
        /// <returns>A new builder instance for the child element.</returns>
        ISchemaElementBuilder CreateElement(XName name, string description = null, Action<ISchemaElement> configurator = null);
        /// <summary>
        /// Creates a new child element which has a value.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="configurator"></param>
        /// <returns>A new builder instance for the child element.</returns>
        ISchemaElementBuilder CreateElementWithValue(XName name, IDataType valueType, string description = null, Action<ISchemaElement> configurator = null);
        /// <summary>
        /// Creates a new attribute to the current element.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="description"></param>
        /// <param name="configurator"></param>
        /// <returns>This builder instance.</returns>
        ISchemaElementBuilder CreateAttribute(XName name, IDataType dataType, string description = null, Action<ISchemaElementAttribute> configurator = null);
        /// <summary>
        /// Returns a new builder for the parent element.
        /// </summary>
        /// <returns></returns>
        ISchemaElementBuilder Parent();
        /// <summary>
        /// Returns the current schema element.
        /// </summary>
        ISchemaElement Element();
    }
}
