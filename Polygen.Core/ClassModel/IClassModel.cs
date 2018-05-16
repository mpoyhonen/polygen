using System.Collections.Generic;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Core.ClassModel
{
    /// <summary>
    /// Interface for a class design model.
    /// </summary>
    public interface IClassModel: IDesignModel
    {
        /// <summary>
        /// Design model type.
        /// </summary>
        string Type { get; }
        
        /// <summary>
        /// Design model attributes.
        /// </summary>
        IEnumerable<IClassAttribute> Attributes { get; }
        /// <summary>
        /// Incoming relations for this design model.
        /// </summary>
        IEnumerable<IClassRelation> IncomingRelations { get; }
        /// <summary>
        /// Outgoing relations for this design model.
        /// </summary>
        IEnumerable<IClassRelation> OutgoingRelations { get; }
        /// <summary>
        /// Creates a clone of the given attribute and adds it into this model.
        /// </summary>
        /// <param name="sourceAttribute"></param>
        /// <param name="parseLocation"></param>
        /// <returns></returns>
        IClassAttribute CloneAttribute(IClassAttribute sourceAttribute, IParseLocationInfo parseLocation);
        /// <summary>
        /// Adds an attribute to this class model.
        /// </summary>
        /// <param name="attribute"></param>
        void AddAttribute(IClassAttribute attribute);
        /// <summary>
        /// Creates a clone of the given relation and adds it into this model.
        /// </summary>
        /// <param name="sourceRelation"></param>
        /// <param name="destinationClassModel"></param>
        /// <param name="parseLocation"></param>
        /// <returns></returns>
        IClassRelation CloneRelation(IClassRelation sourceRelation, IClassModel destinationClassModel, IParseLocationInfo parseLocation);
        /// <summary>
        /// Adds a relation to this class model.
        /// </summary>
        /// <param name="relation"></param>
        void AddOutgoingRelation(IClassRelation relation);
        /// <summary>
        /// Adds a relation to this class model.
        /// </summary>
        /// <param name="relation"></param>
        void AddIncomingRelation(IClassRelation relation);
        /// <summary>
        /// Called when the references to other class models should be resolved. Called during parsing.
        /// </summary>
        /// <param name="designModelCollection"></param>
        void ResolveReferences(IDesignModelCollection designModelCollection);
    }
    
    public class ClassModelReference : DesignModelReference<IClassModel>
    {
        public ClassModelReference(string name, INamespace ns, string type, IParseLocationInfo parseLocation = null) : base(name, ns, type, parseLocation)
        {
        }
    }
}