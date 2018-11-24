using System.Collections.Generic;
using System.IO;
using Polygen.Core.DesignModel;
using Polygen.Core.Project;
using Polygen.Core.Schema;

namespace Polygen.Core.Parser
{
    /// <summary>
    /// Interface for parsing desing models from an XML element.
    /// </summary>
    public interface IDesignModelParser
    {
        /// <summary>
        /// Adds to the collection all design models parsed from the given XML element and possible all child elements.
        /// </summary>
        /// <param name="rootElement"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        void Parse(IXmlElement rootElement, IDesignModelCollection collection);
    }

    /// <summary>
    /// Contains state during design model parsing.
    /// </summary>
    public class DesignModelParseContext
    {
        public DesignModelParseContext(IDesignModelCollection collection)
        {
            Collection = collection;
        }

        public DesignModelParseContext(DesignModelParseContext parent)
        {
            Collection = parent.Collection;
            XmlElement = parent.XmlElement;
            DesignModel = parent.DesignModel;
            Namespace = parent.Namespace;
        }

        /// <summary>
        /// Current XML element which is being parsed.
        /// </summary>
        public IXmlElement XmlElement { get; set; }
        /// <summary>
        /// Last parsed desing model.
        /// </summary>
        public IDesignModel DesignModel { get; set; }
        /// <summary>
        /// Current namespace.
        /// </summary>
        public INamespace Namespace { get; set; }
        /// <summary>
        /// Contains all namespaces and design models.
        /// </summary>
        public IDesignModelCollection Collection { get; }
    }
}
