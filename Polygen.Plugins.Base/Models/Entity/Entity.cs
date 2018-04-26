using System.Collections.Generic;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;
using Polygen.Plugins.Base.ModelContract;
using Polygen.Plugins.Base.Models.Relation;

namespace Polygen.Plugins.Base.Models.Entity
{
    public class Entity : Core.Impl.DesignModel.DesignModelBase, IModel
    {
        private readonly List<EntityAttribute> _attributes = new List<EntityAttribute>();
        private readonly List<EntityRelation> _incomingRelations = new List<EntityRelation>();
        private readonly List<EntityRelation> _outgoingRelations = new List<EntityRelation>();

        public Entity(INamespace ns, IXmlElement element = null) : base("Entity", ns, element)
        {
            this.Name = element?.GetAttribute("name")?.Value;
        }

        public IEnumerable<IAttribute> Attributes => this._attributes;
        public IEnumerable<IModelRelation> IncomingRelations => this._incomingRelations;
        public IEnumerable<IModelRelation> OutgoingRelations => this._outgoingRelations;
        
        public void CopyFrom(IModel source)
        {
            Name = source.Name;
            
            foreach (var attribute in source.Attributes)
            {
                AddAttribute(new EntityAttribute(this, attribute));
            }
        }

        public void CopyRelations(IEnumerable<(IModelRelation, IModel)> relationData)
        {
            foreach (var (sourceRelation, destinationModel) in relationData)
            {
                if (destinationModel is Entity destinationEntity)
                {
                    var relation = new EntityRelation(this, destinationEntity, Element);
                    
                    AddOutgoingRelation(relation);
                    destinationEntity.AddIncomingRelation(relation);
                }
                else
                {
                    throw new ParseException(Element.ParseLocation, "Relation destination model must be an Entity.");
                }
            }
        }

        public void AddAttribute(EntityAttribute entityAttribute)
        {
            this._attributes.Add(entityAttribute);
        }

        public void AddOutgoingRelation(EntityRelation relation)
        {
            this._outgoingRelations.Add(relation);
        }
        
        public void AddIncomingRelation(EntityRelation relation)
        {
            this._incomingRelations.Add(relation);
        }
    }
}
