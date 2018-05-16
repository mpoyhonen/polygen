using System.Collections.Generic;
using Polygen.Core.ClassModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.ClassModelMapping
{
    /// <summary>
    /// Defines a way to map attributes and relations from one class-like design model to another.
    /// </summary>
    public class ClassMapping
    {
        /// <summary>
        /// Optional name for this mapping. Can be used to define multiple mappings for the same types.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Source design model reference.
        /// </summary>
        public ClassModelReference Source { get; set;}

        /// <summary>
        /// Destination design reference.
        /// </summary>
        public ClassModelReference Destination { get; set; }

        /// <summary>
        /// Whether to map attributes from the source to the destination.
        /// </summary>
        public bool MapAttributes { get; set;}

        /// <summary>
        /// Whether to map all relations from the source to the destination.
        /// </summary>
        public bool MapRelations { get; set;}

        /// <summary>
        /// Whether to all attributes and relations which exists at the source and not at the destination.
        /// </summary>
        public bool AddMissing { get; set;}

        /// <summary>
        /// Whether to to create a mapping for the opposite direction.
        /// </summary>
        public bool TwoWay { get; set; }
        
        /// <summary>
        /// Names of attributes which are not mapped.
        /// </summary>
        private ISet<string> SkippedAttributeNames { get; } = new HashSet<string>();

        /// <summary>
        /// Contains all configured attribute mappings for this class mapping.
        /// </summary>
        public List<AttributeMapping> AttributeMappings { get; } = new List<AttributeMapping>();
        
        /// <summary>
        /// Names of relations which are not mapped.
        /// </summary>
        private ISet<string> SkippedRelationNames { get; } = new HashSet<string>();

        /// <summary>
        /// Contains all configured relation mappings for this class mapping.
        /// </summary>
        public List<RelationMapping> RelationMappings { get; } = new List<RelationMapping>();
        
        /// <summary>
        /// Mapping element parse location for error messages.
        /// </summary>
        public IParseLocationInfo ParseLocation { get; set; }
    }
}