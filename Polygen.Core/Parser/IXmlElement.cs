using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using Polygen.Core.Schema;

namespace Polygen.Core.Parser
{
    /// <summary>
    /// Interface for an element parsed from XML using a schema element.
    /// </summary>
    public interface IXmlElement
    {
        /// <summary>
        /// Schema element this model was parsed from.
        /// </summary>
        ISchemaElement Definition { get; }
        /// <summary>
        /// Parent XML element.
        /// </summary>
        IXmlElement ParentElement { get; }
        /// <summary>
        /// Contains parse location information for error messages.
        /// </summary>
        IParseLocationInfo ParseLocation { get; }
        /// <summary>
        /// Element value set for models where the value is the text inside the XML element.
        /// </summary>
        string Value { get; set; }
        /// <summary>
        /// Parsed attributes.
        /// </summary>
        IEnumerable<IXmlElementAttribute> Attributes { get; }
        /// <summary>
        /// Child XML elements.
        /// </summary>
        IEnumerable<IXmlElement> Children { get; }
        /// <summary>
        /// Namespace this XML element was parsed from.
        /// </summary>
        INamespace Namespace { get; }
        /// <summary>
        /// Parsed design model.
        /// </summary>
        IDesignModel DesignModel { get; set; }
        /// <summary>
        /// Adds an attribute to this element.
        /// </summary>
        /// <param name="attribute"></param>
        void AddAttribute(IXmlElementAttribute attribute);
        /// <summary>
        /// Adds child element to this element.
        /// </summary>
        /// <param name="childElement"></param>
        void AddChildElement(IXmlElement childElement);
        /// <summary>
        /// Returns an attribute with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IXmlElementAttribute GetAttribute(string name);
        /// <summary>
        /// Returns the first matching child element (path can contain subpaths separated by '/').
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IXmlElement FindChildElement(string path);
    }
}
