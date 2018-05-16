using System.Collections.Generic;
using Polygen.Core.ClassModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.ClassModelDerivation
{
    /// <summary>
    /// Defines a way to copy attributes and relations from one class-like design model.
    /// </summary>
    public class ClassDerivation
    {
        /// <summary>
        /// Source design model reference.
        /// </summary>
        public ClassModelReference Source { get; set; }

        /// <summary>
        /// Destination design model reference.
        /// </summary>
        public ClassModelReference Destination { get; set; }

        /// <summary>
        /// Whether to copy all attributes.
        /// </summary>
        public bool CopyAttributes { get; set;}

        /// <summary>
        /// Whether to copy all relations.
        /// </summary>
        public bool CopyRelations { get; set;}
        
        /// <summary>
        /// Names of attributes which are going to be skipped.
        /// </summary>
        public ISet<string> SkippedAttributeNames { get; } = new HashSet<string>();

        /// <summary>
        /// Contains all configured attribute derivations for this class derivation.
        /// </summary>
        public List<AttributeDerivation> AttributeDerivations { get; } = new List<AttributeDerivation>();
        
        /// <summary>
        /// Names of relations which are going to be skipped.
        /// </summary>
        public ISet<string> SkippedRelationNames { get; } = new HashSet<string>();

        /// <summary>
        /// Contains all configured relation derivation for this class derivation.
        /// </summary>
        public List<RelationDerivation> RelationDerivations { get; } = new List<RelationDerivation>();
        
        /// <summary>
        /// Derivation element parse location for error messages.
        /// </summary>
        public IParseLocationInfo ParseLocation { get; set; }
    }
}