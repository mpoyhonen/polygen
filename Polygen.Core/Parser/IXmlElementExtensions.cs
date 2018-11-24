using System;
using System.Xml.Linq;
using Polygen.Core.Exceptions;

// ReSharper disable once InconsistentNaming

namespace Polygen.Core.Parser
{
    /// <summary>
    /// Extension methods for IXmlElement interface.
    /// </summary>
    public static class IXmlElementExtensions
    {
        /// <summary>
        /// Returns a string attribute value.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string GetStringAttributeValue(this IXmlElement element, string name, string defaultValue = null)
        {
            return element.GetAttribute(name)?.Value ?? defaultValue;
        }
        
        /// <summary>
        /// Returns a boolean attribute value. Valid values: true, false, yes, no
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool GetBoolAttributeValue(this IXmlElement element, string name, bool defaultValue = false)
        {
            var value = element.GetStringAttributeValue(name, defaultValue ? "true" : "false");

            switch (value.ToLower())
            {
                case "true":
                case "yes":
                    return true;
                case "false":
                case "no":
                    return false;
                default:
                    throw new ParseException(element.ParseLocation, $"Invalid boolean value: {value}");
            }
        }
        
        /// <summary>
        /// Returns an integer attribute value.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int GetIntAttributeValue(this IXmlElement element, string name, int defaultValue = 0)
        {
            var value = element.GetStringAttributeValue(name);

            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            try
            {
                return int.Parse(value);
            }
            catch (Exception e)
            {
                throw new ParseException(element.ParseLocation, $"Invalid integer value: {value}", e);
            }
        }
    }
}
