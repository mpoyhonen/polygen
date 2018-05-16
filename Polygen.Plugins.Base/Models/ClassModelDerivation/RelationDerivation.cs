using System.Collections.Generic;
using Polygen.Core.ClassModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.ClassModelDerivation
{
    /// <summary>
    /// Defines how to derive a relation from one class-like design model relation.
    /// </summary>
    public class RelationDerivation
    {
        /// <summary>
        /// Source relation reference.
        /// </summary>
        public ClassRelationReference Source { get; set; }
        
        /// <summary>
        /// Destination relation reference.
        /// </summary>
        public ClassRelationReference Destination { get; set; }
        
        /// <summary>
        /// Whether to copy attributes from the source.
        /// </summary>
        public bool MapAttributes { get; set; }

        /// <summary>
        /// Names of attribues which are going to be skipped.
        /// </summary>
        private ISet<string> SkippedAttributeNames { get; } = new HashSet<string>();

        /// <summary>
        /// Contains all configured attribute derivations.
        /// </summary>
        public List<AttributeDerivation> AttributeDerivations { get; } = new List<AttributeDerivation>();
        
        /// <summary>
        /// Derivation element parse location for error messages.
        /// </summary>
        public IParseLocationInfo ParseLocation { get; set; }
    }
}