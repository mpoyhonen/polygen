using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Polygen.Core.Parser;

namespace Polygen.Core.Exceptions
{
    public class ParseException : Exception
    {
        public ParseException(IParseLocationInfo locationInfo, string message)
            : base(FormatMessage(locationInfo, message))
        {
        }

        public ParseException(IParseLocationInfo locationInfo, string message, Exception innerException)
            : base(FormatMessage(locationInfo, message), innerException)
        {
        }

        private static string FormatMessage(IParseLocationInfo locationInfo, string message)
        {
            var buf = new StringBuilder(512);

            buf.Append(message);
            buf.Append(" (").Append(locationInfo).Append(")");

            return buf.ToString();
        }
    }
}
