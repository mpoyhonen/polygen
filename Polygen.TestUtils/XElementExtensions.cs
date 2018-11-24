using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Polygen.TestUtils
{
    /// <summary>
    /// Test helper extension methods for the XElement.
    /// </summary>
    public static class XElementExtensions
    {
        /// <summary>
        /// Creates a ordered string reprensentation of the XML. Note, currently this
        /// might not return 100% correct XML.
        /// </summary>
        /// <param name="rootElement"></param>
        /// <returns></returns>
        public static string ToTestString(this XElement rootElement)
        {
            var buf = new StringBuilder(10000);

            void AppendIndent(int indent)
            {
                for (var i = 0; i < indent; i++)
                {
                    buf.Append("    ");
                }
            }
            string GetNameWithPrefix(XName name, Dictionary<string, string> prefixMap)
            {
                if (prefixMap.TryGetValue(name.NamespaceName, out var prefix))
                {
                    return prefix + ":" + name.LocalName;
                }

                return name.LocalName;
            }
            void ParseNamespaceDeclarations(XElement element, Dictionary<string, string> prefixMap)
            {
                var namespaceDeclarations = element
                    .Attributes()
                    .Where(x => x.Name.NamespaceName == XNamespace.Xmlns)
                    .OrderBy(x => x.Name.LocalName);

                foreach (var attrib in namespaceDeclarations)
                {
                    prefixMap.Remove(attrib.Value);
                    prefixMap.Add(attrib.Value, attrib.Name.LocalName);
                }
            }
            void AppendAttributes(XElement element, Dictionary<string, string> prefixMap)
            {
                var namespaceDeclarations = element
                    .Attributes()
                    .Where(x => x.Name.NamespaceName == XNamespace.Xmlns)
                    .OrderBy(x => x.Name.LocalName);
                var normalAttributes = element
                    .Attributes()
                    .Where(x => x.Name.NamespaceName != XNamespace.Xmlns)
                    .OrderBy(x => x.Name.LocalName);

                foreach (var attrib in normalAttributes)
                {
                    buf.Append($" {GetNameWithPrefix(attrib.Name.LocalName, prefixMap)}='{attrib.Value}'");
                }

                foreach (var attrib in namespaceDeclarations)
                {
                    buf.Append($" xmlns:{attrib.Name.LocalName}='{attrib.Value}'");
                }
            }
            void Append(XElement element, int indent, Dictionary<string, string> prefixMap)
            {
                ParseNamespaceDeclarations(element, prefixMap);

                var elementName = GetNameWithPrefix(element.Name, prefixMap);

                AppendIndent(indent);
                buf.Append($"<{elementName}");
                AppendAttributes(element, prefixMap);

                if (!element.Elements().Any())
                {
                    if (!string.IsNullOrWhiteSpace(element.Value))
                    {
                        buf.Append($">\r\n");
                        AppendIndent(indent + 1);
                        buf.Append($"{ element.Value ?? ""}\r\n");
                        AppendIndent(indent);
                        buf.Append($"</{elementName}>\r\n");
                    }
                    else
                    {
                        buf.Append($"/>\r\n");
                    }
                }
                else
                {
                    buf.Append($">\r\n");

                    foreach (var childElement in element.Elements())
                    {
                        Append(childElement, indent + 1, new Dictionary<string, string>(prefixMap));
                    }

                    AppendIndent(indent);
                    buf.Append($"</{elementName}>\r\n");
                }
            }

            Append(rootElement, 0, new Dictionary<string, string>());

            return buf.ToString();
        }

        public static string FixNewlines(this string str)
        {
            return str.Replace("\r\n", "\n");
        }
    }
}
