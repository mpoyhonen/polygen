using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;
using Polygen.Core.Schema;

namespace Polygen.Core.Impl.DesignModel
{
    public class XmlElement : IXmlElement
    {
        private readonly List<IXmlElementAttribute> _attributes = new List<IXmlElementAttribute>();
        private readonly List<IXmlElement> _children = new List<IXmlElement>();

        public XmlElement(ISchemaElement definition, IXmlElement parentElement, IParseLocationInfo parseLocation)
        {
            this.Definition = definition;
            this.ParentElement = parentElement;
            this.ParseLocation = parseLocation;
        }

        public ISchemaElement Definition { get; }
        public IXmlElement ParentElement { get; }
        public IParseLocationInfo ParseLocation { get; }
        public string Value { get; set; }
        public INamespace Namespace { get; set; }

        public IEnumerable<IXmlElementAttribute> Attributes => this._attributes;
        public IEnumerable<IXmlElement> Children => this._children;
        public IDesignModel DesignModel { get; set; }

        public IXmlElementAttribute GetAttribute(string name)
        {
            return this._attributes.Find(x => x.Definition.Name.LocalName == name);
        }

        public IEnumerable<IXmlElement> GetChildElments(XName name)
        {
            return this._children.Where(x => x.Definition.Name.LocalName == name);
        }

        public IXmlElement FindChildElement(string path)
        {
            var pos = path.IndexOf('/');
            var name = pos > 0 ? path.Substring(0, pos) : path;
            var match = this._children.Find(x => x.Definition.Name.LocalName == name);

            if (match == null)
            {
                return null;
            }

            return pos > 0 ? match.FindChildElement(path.Substring(pos + 1)) : match;
        }

        public void AddAttribute(IXmlElementAttribute attribute)
        {
            if (this._attributes.Any(x => x.Definition.Name == attribute.Definition.Name))
            {
                throw new DesignModelElementException(this, $"Attribute '{attribute.Definition.Name}' is already defined for this element.");
            }

            this._attributes.Add(attribute);
        }

        public void AddChildElement(IXmlElement childElement)
        {
            if (!childElement.Definition.AllowMultiple && this._children.Any(x => x.Definition.Name == childElement.Definition.Name))
            {
                throw new DesignModelElementException(this, $"Child element '{childElement.Definition.Name.LocalName}' is already defined for this element.");
            }

            this._children.Add(childElement);
        }

        public void Validate()
        {
            foreach (var attributeDef in this.Definition.Attributes)
            {
                if (attributeDef.IsMandatory && !this._attributes.Any(x => x.Definition.Name.LocalName == attributeDef.Name.LocalName))
                {
                    throw new DesignModelElementException(this, $"Attribute '{attributeDef.Name.LocalName}' is must be defined for this element.");
                }
            }

            foreach (var childElementDef in this.Definition.Children)
            {
                if (childElementDef.IsMandatory && !this._children.Any(x => x.Definition.Name == childElementDef.Name))
                {
                    throw new DesignModelElementException(this, $"Child element '{childElementDef.Name.LocalName}' is must be defined for this element.");
                }
            }
        }
    }
}
