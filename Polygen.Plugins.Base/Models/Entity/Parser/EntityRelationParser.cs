using System.Xml;
using Polygen.Core.ClassModel;
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
        {
            _designModelCollection = designModelCollection;
        }

        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            var sourceEntity = (Entity) context.DesignModel;
            var name = xmlElement.GetStringAttributeValue("name");
            var relation = new EntityRelation(name, xmlElement, xmlElement.ParseLocation)
            {
                Source = sourceEntity,
                DestinationReference = new ClassModelReference(xmlElement.GetStringAttributeValue("destination"),
                    sourceEntity.Namespace, sourceEntity.Type, xmlElement.ParseLocation)
            };

            sourceEntity.AddOutgoingRelation(relation);

            // Don't register this design model globally.
            return null;
        }
    }
}
