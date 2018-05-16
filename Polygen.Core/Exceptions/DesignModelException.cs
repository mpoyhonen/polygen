using System;
using System.Text;
using System.Xml.Linq;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Core.Exceptions
{
    public class DesignModelException : CodeGenerationException
    {
        public DesignModelException(IDesignModel model, string message)
            : base(FormatMessage(model.Element.Definition.Name, model.ParseLocation, message))
        {
        }

        public DesignModelException(IDesignModel model, string message, Exception innerException)
             : base(FormatMessage(model.Element.Definition.Name, model.ParseLocation, message), innerException)
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
