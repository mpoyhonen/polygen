using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Polygen.Core.Exceptions;

namespace Polygen.Core.Utils
{
    /// <summary>
    /// Implements a glob expression filter. Supports include and exclude expressions.
    /// </summary>
    public class Filter
    {
        private List<Tuple<Regex, bool, string>> _entries = new List<Tuple<Regex, bool, string>>();
        private char _pathSeparator;

        public Filter(char pathSeparator = '/')
        {
            _pathSeparator = pathSeparator;
        }

        public Filter(string includeExpr, char pathSeparator = '/'): this(pathSeparator)
        {
            AddInclude(includeExpr);
        }

        /// <summary>
        /// Adds a new include pattern.
        /// </summary>
        /// <param name="expr"></param>
        public void AddInclude(string expr)
        {
            var regex = StringUtils.ConvertGlobToRegex(expr, _pathSeparator);

            if (regex == null)
            {
                throw new ConfigurationException("Invalid glob pattern: " + expr);
            }

            _entries.Add(Tuple.Create(regex, true, expr));
        }

        /// <summary>
        /// Adds a new exclude patttern.
        /// </summary>
        /// <param name="expr"></param>
        public void AddExclude(string expr)
        {
            var regex = StringUtils.ConvertGlobToRegex(expr, _pathSeparator);

            if (regex == null)
            {
                throw new ConfigurationException("Invalid glob pattern: " + expr);
            }

            _entries.Add(Tuple.Create(regex, false, expr));
        }

        /// <summary>
        /// Tries to match the given string agains the configured patterns.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public MatchStatus Match(string str)
        {
            var result = MatchStatus.None;

            foreach (var entry in _entries)
            {
                if (!entry.Item1.IsMatch(str))
                {
                    continue;
                }

                if (entry.Item2)
                {
                    result = MatchStatus.Included;
                }
                else
                {
                    result = MatchStatus.Excluded;
                    break;
                }
            }

            return result;
        }
        /// <summary>
        /// Indicates the filter match result.
        /// </summary>
        public enum MatchStatus
        {
            None,
            Included,
            Excluded
        }

        /// <summary>
        /// Checks if the filter expressions are equal to the other filter.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool FilterEquals(Filter other)
        {
            if (_entries.Count != other._entries.Count)
            {
                return false; 
            }

            for (var i = 0; i < _entries.Count; i++)
            {
                var a = _entries[i];
                var b = other._entries[i];

                if (a.Item3 != b.Item3 || a.Item2 != b.Item2)
                {
                    return false;
                }

            }

            return true;
        }

        public override string ToString()
        {
            return string.Join(", ", _entries.Select(x => (x.Item2 ? "Include:" : "Exclude") + x.Item3));
        }
    }
}
