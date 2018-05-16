using System.Linq;
using Polygen.Core.ClassModel;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;

// ReSharper disable InconsistentNaming

namespace Polygen.Plugins.Base.ClassDesignModel
{
    /// <summary>
    /// Extension methods for IClassDesignModel interface.
    /// </summary>
    public static class IClassDesignModelExtensions
    {
        /// <summary>
        /// Returns an attribute with the given name. Throws an exception if not found.
        /// </summary>
        /// <param name="designModel"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IClassAttribute GetAttribute(this IClassModel designModel, string name)
        {
            var attribute = designModel.Attributes.FirstOrDefault(x => x.Name == name);

            if (attribute == null)
            {
                throw new ParseException(((IDesignModel) designModel).ParseLocation, $"Attribute '{name}' not found in design model '{designModel.FullyQualifiedName}'");
            }

            return attribute;
        }
        
        /// <summary>
        /// Returns an outgoing relation with the given name. Throws an exception if not found.
        /// </summary>
        /// <param name="designModel"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IClassRelation GetOutgoingRelation(this IClassModel designModel, string name)
        {
            var relation = designModel.OutgoingRelations.FirstOrDefault(x => x.Name == name);

            if (relation == null)
            {
                throw new ParseException(((IDesignModel) designModel).ParseLocation, $"Relation '{name}' not found in design model '{designModel.FullyQualifiedName}'");
            }

            return relation;
        }
    }
}