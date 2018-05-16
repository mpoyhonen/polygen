using System;
using System.Collections.Generic;
using System.Linq;
using Polygen.Core.ClassModel;
using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Impl.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Core.Impl.ClassModel
{
    public class ClassModel: DesignModelBase, IClassModel
    {
        private readonly List<IClassAttribute> _attributes = new List<IClassAttribute>();
        private readonly List<IClassRelation> _incomingRelations = new List<IClassRelation>();
        private readonly List<IClassRelation> _outgoingRelations = new List<IClassRelation>();
        
        public ClassModel(string name, string type, INamespace ns, IXmlElement element = null, IParseLocationInfo parseLocation = null)
            : base(name, type, ns, element, parseLocation)
        {
        }

        public string Type { get; }
        public IEnumerable<IClassAttribute> Attributes => _attributes;
        public IEnumerable<IClassRelation> IncomingRelations => _incomingRelations;
        public IEnumerable<IClassRelation> OutgoingRelations => _outgoingRelations;

        /// <summary>
        /// Creates an attribute. Subclasses can override to use specific types.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="element"></param>
        /// <param name="parseLocation"></param>
        /// <returns></returns>
        protected virtual IClassAttribute CreateAttribute(string name, IDataType type, IXmlElement element, IParseLocationInfo parseLocation)
        {
            return new ClassAttribute(name, type, element, parseLocation);
        }
        
        public virtual IClassAttribute CloneAttribute(IClassAttribute sourceAttribute, IParseLocationInfo parseLocation)
        {
            var attribute = CreateAttribute(sourceAttribute.Name, sourceAttribute.Type, null, parseLocation: parseLocation);
            
            attribute.CopyPropertiesFrom(sourceAttribute, parseLocation);
            AddAttribute(attribute);

            return attribute;
        }

        public void AddAttribute(IClassAttribute attribute)
        {
            if (_attributes.Any(x => x.Name == attribute.Name))
            {
                throw new DesignModelException(this, $"Design model already contains attribute '{attribute.Name}'");
            }

            _attributes.Add(attribute);
        }
        
        /// <summary>
        /// Creates a relation. Subclasses can override to use specific types.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="element"></param>
        /// <param name="parseLocation"></param>
        /// <returns></returns>
        protected virtual IClassRelation CreateRelation(string name, IXmlElement element, IParseLocationInfo parseLocation)
        {
            return new ClassRelation(name, element, parseLocation);
        }

        public void AddOutgoingRelation(IClassRelation relation)
        {
            if (_outgoingRelations.Any(x => x.Name == relation.Name))
            {
                throw new DesignModelException(this, $"Design model already contains relation '{relation.Name}'");
            }

            _outgoingRelations.Add(relation);
        }
        
        public void AddIncomingRelation(IClassRelation relation)
        {
            if (_incomingRelations.Any(x => x.Name == relation.Name))
            {
                throw new DesignModelException(this, $"Design model already contains relation '{relation.Name}'");
            }

            _incomingRelations.Add(relation);
        }

        public void ResolveReferences(IDesignModelCollection designModelCollection)
        {
            _outgoingRelations.ForEach(relation =>
            {
                relation.ResolveReferences(designModelCollection);
                relation.Destination.AddIncomingRelation(relation);
            });
        }

        public virtual IClassRelation CloneRelation(IClassRelation sourceRelation, IClassModel destinationClassModel, IParseLocationInfo parseLocation)
        {
            var relation = new ClassRelation(sourceRelation.Name, parseLocation: parseLocation);
            
            relation.CopyPropertiesFrom(sourceRelation, parseLocation);
            relation.Destination = destinationClassModel;
            AddOutgoingRelation(relation);
            destinationClassModel.AddIncomingRelation(relation);

            return relation;
        }
    }
}