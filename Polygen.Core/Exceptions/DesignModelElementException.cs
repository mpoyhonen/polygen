using System;
using System.Text;
using System.Xml.Linq;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Core.Exceptions
{
    public class DesignModelElementException : CodeGenerationException
    {
        public DesignModelElementException(IXmlElement model, string message)
            : base(FormatMessage(model.Definition.Name, model.ParseLocation, message))
        {
        }

        public DesignModelElementException(IXmlElement model, string message, Exception innerException)
             : base(FormatMessage(model.Definition.Name, model.ParseLocation, message), innerException)
        {
        }

        public DesignModelElementException(IXmlElementAttribute attribute, string message)
            : base(FormatMessage(attribute.Definition.Element.Name, attribute.ParseLocation, message))
        {
        }

        public DesignModelElementException(IXmlElementAttribute attribute, string message, Exception innerException)
            : base(FormatMessage(attribute.Definition.Element.Name, attribute.ParseLocation, message), innerException)
        {
        }

        private static string FormatMessage(XName xmlName, IParseLocationInfo parseLocation, string message)
        {
            var buf = new StringBuilder(512);

            buf.Append("Error in ");
            buf.Append(xmlName.LocalName);
            buf.Append(": ");

            buf.Append(message);
            buf.Append(" (").Append(parseLocation).Append(")");

            return buf.ToString();
        }
    }
}
