using System.Collections.Generic;
using Polygen.Core.ClassModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.ClassModelMapping
{
    /// <summary>
    /// Defines how to map a relation in one from one class-like design model relation to another class-like design model.
    /// </summary>
    public class RelationMapping
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
        /// Whether to map attributes from the source to the destination.
        /// </summary>
        public bool MapAttributes { get; set; }

        /// <summary>
        /// Whether to all attributes which exists at the source and not at the destination.
        /// </summary>
        public bool AddMissing { get; set; }

        /// <summary>
        /// Whether to to create a mapping for the opposite direction.
        /// </summary>
        public bool TwoWay { get; set; }

        /// <summary>
        /// Names of attribues which are not mapped.
        /// </summary>
        private ISet<string> SkippedAttributeNames { get; } = new HashSet<string>();

        /// <summary>
        /// Contains all configured attribute mappings for this mapping.
        /// </summary>
        public List<AttributeMapping> AttributeMappings { get; } = new List<AttributeMapping>();
        
        /// <summary>
        /// Mapping element parse location for error messages.
        /// </summary>
        public IParseLocationInfo ParseLocation { get; set; }
    }
}