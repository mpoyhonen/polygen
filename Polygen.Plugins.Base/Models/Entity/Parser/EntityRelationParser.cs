using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Parser
{
    /// <summary>
    /// EntityRelation design model parser.
    /// </summary>
    public class EntityRelationParser: DesignModelGeneratorBase
    {
        private readonly IDesignModelCollection _designModelCollection;

        public EntityRelationParser(IDesignModelCollection designModelCollection) 
            : base(nameof(EntityRelation), new[] { nameof(Entity) })
        {
            _designModelCollection = designModelCollection;
        }

        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            var source = (Entity)context.DesignModel;
            var destination = (Entity)_designModelCollection.GetDesignModel("Entity", xmlElement.GetAttribute("destination")?.Value, xmlElement.ParseLocation);
            var relation = new EntityRelation(source, destination, xmlElement);
                    
            source.AddOutgoingRelation(relation);
            destination.AddIncomingRelation(relation);

            return relation;
        }
    }
}
