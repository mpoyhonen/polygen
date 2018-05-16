using System.Collections.Generic;
using Polygen.Core.DesignModel;
using Polygen.Core.Impl.Reference;
using Polygen.Core.Parser;

namespace Polygen.Core.ClassModel
{
    /// <summary>
    /// Provider access to data of a class-line design models which attribute.
    /// </summary>
    public interface IClassRelation: IDesignModel
    {
        /// <summary>
        /// Relation source design model.
        /// </summary>
        IClassModel Source { get; }
        /// <summary>
        /// Relation destination design model.
        /// </summary>
        IClassModel Destination { get; }
        /// <summary>
        /// Relation attributes.
        /// </summary>
        IEnumerable<IClassAttribute> Attributes { get; }
        /// <summary>
        /// Creates a clone of the given attribute and adds it into this relation.
        /// </summary>
        /// <param name="sourceAttribute"></param>
        /// <param name="parseLocation"></param>
        /// <returns></returns>
        IClassAttribute CloneAttribute(IClassAttribute sourceAttribute, IParseLocationInfo parseLocation);
        /// <summary>
        /// Adds an attribute to this relation..
        /// </summary>
        /// <param name="attribute"></param>
        void AddAttribute(IClassAttribute attribute);       
        /// <summary>
        /// Called when the references to other class models should be resolved. Called during parsing.
        /// </summary>
        /// <param name="designModelCollection"></param>
        void ResolveReferences(IDesignModelCollection designModelCollection);
    }
    
    public class ClassRelationReference: DesignModelReference<IClassRelation>
    {
        public ClassRelationReference(string name, IParseLocationInfo parseLocation) : base(name, null, "relation", parseLocation)
        {
        }
    }    
}