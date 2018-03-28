using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Polygen.Core.DataType;
using Polygen.Core.Exceptions;
using Polygen.Core.Schema;

namespace Polygen.Core.Impl.Schema
{
    public class SchemaElement : ISchemaElement
    {
        private readonly List<ISchemaElementAttribute> _attributes = new List<ISchemaElementAttribute>();
        private readonly List<ISchemaElement> _children = new List<ISchemaElement>();

		public SchemaElement(ISchema schema, XName name, string description, ISchemaElement parent = null)
        {
            if (string.IsNullOrWhiteSpace(name.NamespaceName))
            {
                name = schema.Namespace + name.LocalName;
            }

			this.Schema = schema;
            this.Parent = parent;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Owner schema.
        /// </summary>
        public ISchema Schema { get; }
        /// <summary>
        /// Parent element.
        /// </summary>
        public ISchemaElement Parent { get; }
        /// <summary>
        /// Defines XML element name and namespace.
        /// </summary>
        public XName Name { get; }
        /// <summary>
        /// Documentation for this schema element.
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// Defines the attributes for this element.
        /// </summary>
        public IReadOnlyList<ISchemaElementAttribute> Attributes => this._attributes;
        /// <summary>
        /// Defines the possible child elements for this element.
        /// </summary>
        public IReadOnlyList<ISchemaElement> Children => this._children;

        /// <summary>
        /// Whether this is an optional or mandatory element.
        /// </summary>
        public bool IsMandatory { get; set; }
        /// <summary>
        /// Whether this element can appear more than once under the parent.
        /// </summary>
        public bool AllowMultiple { get; set; }
        /// <summary>
        /// Whether to use the text content of th XML element as the value of this element.
        /// </summary>
        public bool UseContentAsValue { get; set; }
        /// <summary>
        /// Convent value type.
        /// </summary>
        public IDataType ValueType { get; set; }

        /// <summary>
        /// Adds an attribute.
        /// </summary>
        /// <param name="attribute"></param>
        public void AddAttribute(ISchemaElementAttribute attribute)
        {
            if (this._attributes.Any(x => x.Name == attribute.Name))
            {
                throw new SchemaException($"Attribute '{attribute.Name}' is already defined for schema element '{this.Name}'.");
            }

            if (this._attributes.Any(x => x.Name == attribute.Name))
            {
                throw new SchemaException($"Attribute '{attribute.Name}' is already defined for schema element '{this.Name}'.");
            }

            this._attributes.Add(attribute);
        }

        /// <summary>
        /// Adds a child schema element.
        /// </summary>
        /// <param name="schemaElement"></param>
        public void AddChildElement(ISchemaElement schemaElement)
        {
            if (this.UseContentAsValue)
            {
                throw new SchemaException($"Cannot add a child element to schema element '{this.Name}' as it uses the XML element content as element value.");
            }

            if (this._children.Any(x => x.Name == schemaElement.Name))
            {
                throw new SchemaException($"Child element '{schemaElement.Name}' is already defined for schema element '{this.Name}'.");
            }

            this._children.Add(schemaElement);
        }

        /// <summary>
        /// Removes an attribute.
        /// </summary>
        /// <param name="name"></param>
        public void RemoveAttribute(string name)
        {
            var attribute = this._attributes.Find(x => x.Name == name);

            if (attribute != null)
            {
                this._attributes.Remove(attribute);
            }
        }

        /// <summary>
        /// Removes a child schema element.
        /// </summary>
        /// <param name="name"></param>
        public void RemoveChildElement(string name)
        {
            var childElement = this._children.Find(x => x.Name == name);

            if (childElement != null)
            {
                this._children.Remove(childElement);
            }
        }

        /// <summary>
        /// Returns a new builder for creating new child elements and attributes.
        /// </summary>
        /// <returns></returns>
        public ISchemaElementBuilder GetBuilder()
        {
            return new SchemaElementBuilder(this);
        }

        /// <summary>
        /// Returns the first matching child element (path can contain subpaths separated by '/').
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public ISchemaElement FindChildElement(string path)
        {
            var pos = path.IndexOf('/');
            var name = pos > 0 ? path.Substring(0, pos) : path;
            var match = this.Children.FirstOrDefault(x => x.Name.LocalName == name);

            if (match == null)
            {
                return null;
            }

            return pos > 0 ? match.FindChildElement(path.Substring(pos + 1)) : match;
        }
    }
}
