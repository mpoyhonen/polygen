using System.Collections.Generic;
using System.Linq;
using Polygen.Core.ClassModel;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Impl.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Core.Impl.ClassModel
{
    public class ClassRelation: DesignModelBase, IClassRelation
    {
        private readonly List<IClassAttribute> _attributes = new List<IClassAttribute>();
        
        public ClassRelation(string name, IXmlElement element = null, IParseLocationInfo parseLocation = null) 
            : base(name, nameof(ClassRelation), null, element, parseLocation)
        {
        }

        public IClassModel Source { get; set; }
        public IClassModel Destination { get; set; }
        public ClassModelReference DestinationReference { get; set; }
        public IEnumerable<IClassAttribute> Attributes => _attributes;
        
        public virtual IClassAttribute CloneAttribute(IClassAttribute sourceAttribute, IParseLocationInfo parseLocation)
        {
            var attribute = new ClassAttribute(sourceAttribute.Name, sourceAttribute.Type, null, parseLocation: parseLocation);
            
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
        
        public void ResolveReferences(IDesignModelCollection designModelCollection)
        {
            DestinationReference?.Resolve(designModelCollection);
        }
    }
}