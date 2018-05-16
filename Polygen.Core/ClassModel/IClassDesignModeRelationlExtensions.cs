using System.Linq;
using Polygen.Core.ClassModel;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;

// ReSharper disable InconsistentNaming

namespace Polygen.Plugins.Base.ClassDesignModel
{
    /// <summary>
    /// Extension methods for IClassDesignModelRelation interface.
    /// </summary>
    public static class IClassDesignModelRelationExtensions
    {
        /// <summary>
        /// Returns an attribute with the given name. Throws an exception if not found.
        /// </summary>
        /// <param name="relation"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IClassAttribute GetAttribute(this IClassRelation relation, string name)
        {
            var attribute = relation.Attributes.FirstOrDefault(x => x.Name == name);

            if (attribute == null)
            {
                throw new ParseException(((IDesignModel) relation).ParseLocation, $"Attribute '{name}' not found in relation '{relation.Name}'");
            }

            return attribute;
        }
    }
}