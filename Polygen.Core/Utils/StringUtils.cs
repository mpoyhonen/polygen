using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Polygen.Core.Utils
{
    /// <summary>
    /// String related utility methods.
    /// </summary>
    public static class StringUtils
    {
        /// <summary>
        /// Joins the given strings.
        /// </summary>
        /// <param name="sep">Separator string.</param>
        /// <param name="args">Strings to join</param>
        /// <returns>Joined string</returns>
        public static string JoinWithSeparator(string sep, params string[] args)
        {
            var buf = new StringBuilder(512);

            JoinWithSeparator(buf, sep, args);

            return buf.ToString();
        }

        /// <summary>
        /// Joins the given strings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sep">Separator string.</param>
        /// <param name="e">Strings to join</param>
        /// <returns>Joined string</returns>
        public static string JoinWithSeparator<T>(string sep, IEnumerable<T> e)
        {
            var buf = new StringBuilder(512);

            JoinWithSeparator(buf, sep, e);

            return buf.ToString();
        }

        /// <summary>
        ///  Joins the given strings.
        /// </summary>
        /// <param name="buf">Destination string builder</param>
        /// <param name="sep">Separator string.</param>
        /// <param name="args">Strings to join</param>
        /// <returns>Joined string</returns>
        public static void JoinWithSeparator(StringBuilder buf, string sep, params string[] args)
        {
            JoinWithSeparator<string>(buf, sep, args);
        }

        /// <summary>
        ///  Joins the given strings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buf">Destination string builder</param>
        /// <param name="sep">Separator string.</param>
        /// <param name="e">Strings to join</param>
        /// <returns>Joined string</returns>
        public static void JoinWithSeparator<T>(StringBuilder buf, string sep, IEnumerable<T> e)
        {
            var first = true;

            foreach (var obj in e)
            {
                var arg = obj != null ? obj.ToString() : "";

                if (!string.IsNullOrEmpty(arg))
                {
                    if (!first)
                    {
                        buf.Append(sep);
                    }

                    buf.Append(arg);
                    first = false;
                }
            }
        }

        /// <summary>
        /// Joins the given strings. This version checks if the previous string ends with
        /// the separator, in which case the separator is not added.
        /// This also trims all argument strings.
        /// </summary>
        /// <param name="sep">Separator string.</param>
        /// <param name="args">Strings to join</param>
        /// <returns>Joined string</returns>
        public static string JoinWithSeparatorStrict(string sep, params string[] args)
        {
            var buf = new StringBuilder(512);

            JoinWithSeparatorStrict<string>(buf, sep, args);

            return buf.ToString();
        }

        /// <summary>
        /// Joins the given strings. This version checks if the previous string ends with
        /// the separator or the current starts with it, in which case the separator is not added.
        /// This also trims all argument strings.
        /// </summary>
        /// <param name="buf">Destination string builder</param>
        /// <param name="sep">Separator string.</param>
        /// <param name="args">Strings to join</param>
        /// <returns>Joined string</returns>
        public static void JoinWithSeparatorStrict(StringBuilder buf, string sep, params string[] args)
        {
            JoinWithSeparatorStrict<string>(buf, sep, args);
        }

        /// <summary>
        /// Joins the given strings. This version checks if the previous string ends with
        /// the separator or the current starts with it, in which case the separator is not added.
        /// This also trims all argument strings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buf">Destination string builder</param>
        /// <param name="sep">Separator string.</param>
        /// <param name="e">Strings to join</param>
        /// <returns>Joined string</returns>
        public static void JoinWithSeparatorStrict<T>(StringBuilder buf, string sep, IEnumerable<T> e)
        {
            var prev = (string)null;

            foreach (var arg in e)
            {
                var argTrimmed = arg?.ToString().Trim();

                if (!string.IsNullOrEmpty(argTrimmed))
                {

                    if (prev != null && !prev.EndsWith(sep) && !argTrimmed.StartsWith(sep))
                    {
                        buf.Append(sep);
                    }

                    buf.Append(argTrimmed);
                    prev = argTrimmed;
                }
            }
        }

        /// <summary>
        /// Capilizes the given string.
        /// </summary>
        /// <param name="s">String to capitalize</param>
        /// <param name="onlyFirst">If true, then only the first letter is changed. If false all other letters are set to lower case.</param>
        /// <returns>Capitalized string.</returns>
        public static string Capitalize(string s, bool onlyFirst = false)
        {
            var buf = new StringBuilder(!onlyFirst ? s.ToLowerInvariant() : s);
            var first = s.Length > 0 ? s.Substring(0, 1) : "";
            var rest = s.Length > 1 ? s.Substring(1) : "";

            if (buf.Length > 0)
            {
                buf[0] = char.ToUpperInvariant(buf[0]);
            }

            return buf.ToString();
        }

        /// <summary>
        /// Uncapilizes the given string.
        /// </summary>
        /// <param name="s">String to uncapitalize</param>
        /// <returns>Capitalized string.</returns>
        public static string Uncapitalize(string s)
        {
            return s.Length > 1 ? s.Substring(0, 1).ToLowerInvariant() + s.Substring(1) : s.ToLowerInvariant();
        }

        public static string CamelCaseToUnderScores(string str)
        {
            return CamelCaseToSeparated(str, "_");
        }

        public static string CamelCaseToSeparated(string str, string separator)
        {
            var buf = new StringBuilder(str.Length + 10);
            var prevPos = 0;

#pragma warning disable IDE0007 // Use implicit type
            foreach (Match part in new Regex("([^A-Z])([A-Z])").Matches(str))
#pragma warning restore IDE0007 // Use implicit type
            {
                if (part.Index > prevPos)
                {
                    buf.Append(str.Substring(prevPos, part.Index - prevPos).ToLowerInvariant());
                }

                buf.Append(part.Groups[1].ToString().ToLowerInvariant());
                buf.Append(separator);
                buf.Append(part.Groups[2].ToString().ToLowerInvariant());
                prevPos = part.Index + part.Length;
            }

            if (prevPos < str.Length)
            {
                buf.Append(str.Substring(prevPos, str.Length - prevPos).ToLowerInvariant());
            }

            return buf.ToString();
        }

        public static string ConvertToCamelCase(string str, params char[] separator)
        {
            var parts = str.Split(separator);
            var buf = new StringBuilder(str.Length);

            foreach (var part in parts)
            {
                buf.Append(Capitalize(part));
            }

            return buf.ToString();
        }

        /// <summary>
        /// Converts a file URI to a path or returns the URI if it is not a file URI.
        /// </summary>
        /// <param name="uri">URI to convert</param>
        /// <returns>File path or the original URI</returns>
        public static string ConvertUriToPath(string uri)
        {
            var filePath = uri;

            if (filePath != null && filePath.StartsWith("file" + ":///") && filePath.Length > 8)
            {
                filePath = filePath.Substring(8).Replace('/', '\\');
            }

            return filePath;
        }

        /// <summary>
        /// Resolves path with '..' and '.' in it, e.g. 'a/b/../c' becomes 'a/c' and './a' becomes 'a'.
        /// </summary>
        /// <param name="path">Relative path</param>
        /// <param name="separator">The path separator.</param>
        /// <returns>Added path</returns>
        public static string SimplifyPath(string path, char separator = '/')
        {
            // Handle '..' and '.' segments correctly.
            var stack = new Stack<string>();

            foreach (var part in path.Split(separator).Select(x => x.Trim()))
            {
                if (part == "..")
                {
                    if (stack.Count == 0)
                    {
                        throw new ArgumentException(string.Format("Path '{0}' points outside the root.", path));
                    }

                    stack.Pop();
                }
                else if (part == ".")
                {
                    // Do nothing.
                }
                else
                {
                    stack.Push(part);
                }
            }

            return string.Join("" + separator, stack.ToArray().Reverse());
        }

        /// <summary>
        /// Converts the byte array to a hex string.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] data)
        {
            var sBuilder = new StringBuilder();

            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        /// <summary>
        /// URL encodes the given string.
        /// </summary>
        /// <param name="str">String to URL encode.</param>
        /// <returns>Encoded string.</returns>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var buf = new StringBuilder(str.Length + 50);
            var encoded = false;

            for (int i = 0, count = str.Length; i < count; i++)
            {
                var ch = str[i];

                if ((ch >= 'A' && ch <= 'Z') ||
                    (ch >= 'a' && ch <= 'z') ||
                    (ch >= '0' && ch <= '9') ||
                    (ch == '-' || ch == '_' || ch == '.' || ch == '~'))
                {
                    buf.Append(ch);
                }
                else
                {
                    foreach (var b in Encoding.UTF8.GetBytes(new char[] { ch }))
                    {
                        buf.Append('%').Append(b.ToString("x2"));
                        encoded = true;
                    }
                }
            }

            return encoded ? buf.ToString() : str;
        }

        /// <summary>
        /// Adds the given parameters to the URI as query parameters.
        /// </summary>
        /// <param name="uri">URI</param>
        /// <param name="parameters">parameters to add</param>
        /// <returns>Final URI</returns>
        public static string AddQueryParameters(string uri, params Tuple<string, string>[] parameters)
        {
            var buf = new StringBuilder(1024);
            var hasQuery = uri.LastIndexOf('?') != -1;

            buf.Append(uri);

            foreach (var param in parameters)
            {
                buf.Append(hasQuery ? '&' : '?');
                buf.Append(UrlEncode(param.Item1)).Append('=').Append(UrlEncode(param.Item2));
                hasQuery = true;
            }

            return buf.ToString();
        }

        /// <summary>
        /// Limits the string length to the given value.
        /// </summary>
        /// <param name="str">String</param>
        /// <param name="maxLength">Lenght</param>
        /// <param name="overflowPostfix">(optional) If string is longer than the given length, then this is added to the end.</param>
        /// <returns></returns>
        public static string LimitLength(string str, int maxLength, string overflowPostfix)
        {
            if (str == null || str.Length < maxLength)
            {
                return str;
            }

            var tmp = str.Substring(0, maxLength);

            if (overflowPostfix != null)
            {
                tmp += overflowPostfix;
            }

            return tmp;
        }

        /// <summary>
        /// Converts a glob expression (*.txt, **/*.txt, a?/**/b?c.txt) to a regular expression.
        /// </summary>
        /// <param name="glob">Glob expression to convert,</param>
        /// <returns>Regular expression.</returns>
        public static Regex ConvertGlobToRegex(string glob, char pathSeparator = '/')
        {
            if (string.IsNullOrWhiteSpace(glob))
            {
                return null;
            }

            var buf = new StringBuilder(glob.Length + 10);
            var len = glob.Length;

            buf.Append('^');

            for (var i = 0; i < len; i++)
            {
                var ch = glob[i];

                switch (ch)
                {
                    case '*':
                        if (i < len - 1 && glob[i + 1] == '*')
                        {
                            // Convert ** to .*
                            buf.Append(".*");
                            i++;

                            if (i < len - 1 && glob[i + 1] == pathSeparator)
                            {
                                // Convert **/ to .*/? so that this matches a missing folder too (a/**/b matches a/c/b and a/b)
                                buf.Append(pathSeparator + "?");
                                i++;
                            }
                        }
                        else
                        {
                            buf.Append($"[^{pathSeparator}]+");
                        }
                        break;

                    case '.':
                        // Convert . to \.
                        buf.Append("\\.");
                        break;

                    case '?':
                        // Convert ? to .
                        buf.Append(".");
                        break;

                    case '\\':
                        // Escape the next character.
                        if (i < len - 1)
                        {
                            buf.Append(@"\\").Append(glob[i + 1]);
                            i++;
                        }
                        break;

                    case '(':
                        // Convert (a,b,c) to (:?a|b|c)
                        {
                            var endPos = glob.IndexOf(')', i + 1);

                            if (endPos != -1 && endPos - i > 1)
                            {
                                buf.Append("(:?");
                                foreach (var part in glob.Substring(i + 1, endPos).Split(',', '|'))
                                {
                                    buf.Append(Regex.Escape(part.Trim()));
                                }
                            }
                            else
                            {
                                buf.Append(@"\)");
                            }
                        }
                        break;

                    default:
                        buf.Append(Regex.Escape("" + ch));
                        break;
                }
            }

            buf.Append('$');

            return new Regex(buf.ToString());
        }

        /// <summary>
        /// Splits a string using the given split character (default is space). Quoted parts are preserved.
        /// </summary>
        /// <param name="str">String to split</param>
        /// <param name="quoteChar">Quote characted</param>
        /// <param name="splitChar">Split character</param>
        /// <param name="trimEntries">If true, array entries are trimmed from whitespace.</param>
        /// <param name="converter">Optional function to transform the values</param>
        /// <returns></returns>
        public static string[] SplitWithQuotes(string str, char quoteChar = '"', char splitChar = ' ', bool trimEntries = true, Func<string, string> converter = null)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new string[0];
            }

            var escapedQuote = Regex.Escape("" + quoteChar);
            var regex = new Regex(string.Format("{0}[^{0}]*{0}", escapedQuote));
            var resList = (List<string>)null;
            var prevMatchPos = 0;

#pragma warning disable IDE0007 // Use implicit type
            foreach (Match match in regex.Matches(str))
#pragma warning restore IDE0007 // Use implicit type
            {
                if (resList == null)
                {
                    resList = new List<string>(16);
                }

                if (match.Index > prevMatchPos)
                {
                    resList.AddRange(str.Substring(prevMatchPos, match.Index + prevMatchPos).Split(splitChar));
                }

                resList.Add(match.Groups[0].Value);
                prevMatchPos = match.Index + match.Index;
            }

            if (resList == null)
            {
                resList = new List<string>(16);
            }

            if (prevMatchPos < str.Length)
            {
                resList.AddRange(str.Substring(prevMatchPos, str.Length - prevMatchPos).Split(splitChar));
            }

            var res = (IEnumerable<string>)resList;

            if (trimEntries)
            {
                res = res.Select(x => x.Trim());
            }

            if (converter != null)
            {
                res = res.Select(x => converter(x));
            }

            return res.ToArray();
        }

        /// <summary>
        /// Removes diacritics from a string, e.g. "tëst" becomes "test".
        /// Copied from http://stackoverflow.com/questions/249087/how-do-i-remove-diacritics-accents-from-a-string-in-net)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveDiacritics(string str)
        {
            var stFormD = str.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            for (var i = 0; i < stFormD.Length; i++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[i]);

                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[i]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }
    }
}
