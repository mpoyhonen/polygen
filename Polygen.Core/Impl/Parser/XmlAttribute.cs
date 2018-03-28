﻿using Polygen.Core.Schema;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Core.Impl.DesignModel
{
    public class XmlAttribute : IXmlElementAttribute
    {
        public XmlAttribute(ISchemaElementAttribute definition, string value, IParseLocationInfo parseLocation)
        {
            this.Definition = definition;
            this.Value = value;
            this.ParseLocation = parseLocation;
        }

        public ISchemaElementAttribute Definition { get; }
        public IParseLocationInfo ParseLocation { get; }
        public string Value { get; set; }
    }
}
