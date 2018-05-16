using System.Linq;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;

namespace Polygen.Core.ClassModel
{
    /// <summary>
    /// Helper extension methods for this DesignModel namespace.
    /// </summary>
    public static class Extensions
    {
        public static ClassModelReference CreateClassModelReference(this IDesignModelCollection designModelCollection,
            INamespace ns,
            IParseLocationInfo parseLocation,
            string name, string type = null)
        {
            var pos = name.IndexOf(':');

            if (pos != -1)
            {
                name = name.Substring(0, pos);
                type = name.Substring(pos + 1);
            }

            pos = name.LastIndexOf('.');

            if (pos == -1)
            {
                return new ClassModelReference(name, ns, type, parseLocation);
            }
            
            var nsName = name.Substring(0, pos);

            name = name.Substring(pos + 1);
            ns = designModelCollection.GetNamespace(nsName);

            return new ClassModelReference(name, ns, type, parseLocation);
        }

        public static ClassModelReference CreateClassModelReference(this IClassModel classModel, IParseLocationInfo parseLocation = null)
        {
            var designModel = (IDesignModel) classModel;
            
            return new ClassModelReference(designModel.Name, designModel.Namespace, designModel.DesignModelType, parseLocation ?? designModel.ParseLocation);
        }
        
        public static ClassAttributeReference CreateAttributeReference(this IClassModel classModel, string attributeName, IParseLocationInfo parseLocation = null)
        {
            var designModel = (IDesignModel) classModel;
            
            return new ClassAttributeReference(attributeName, parseLocation ?? designModel.ParseLocation);
        }
        
        public static ClassRelationReference CreateRelationReference(this IClassModel classModel, string relationName, IParseLocationInfo parseLocation)
        {
            return new ClassRelationReference(relationName, parseLocation);
        }
        
        public static ClassAttributeReference CreateRelationAttributeReference(this IClassRelation classRelation, string attributeName, IParseLocationInfo parseLocation)
        {
            return new ClassAttributeReference(attributeName, parseLocation);
        }
        
        public static void Resolve(this ClassModelReference reference, IDesignModelCollection designModelCollection)
        {
            if (reference.Target != null)
            {
                return;
            }
            
            var designModel = reference.Namespace.GetDesignModel(reference, true);
                
            if (!(designModel is IClassModel classDesignModel))
            {
                throw new ParseException(reference.ParseLocation, $"Design model '{reference.Name}' must be a class-like design model.");
            }

            reference.Target = classDesignModel;
        }
        
        public static void Resolve(this ClassAttributeReference reference, IClassModel classModel)
        {
            if (reference.Target != null)
            {
                return;
            }
            
            var attribute = classModel.Attributes.FirstOrDefault(x => x.Name == reference.Name);
                
            if (attribute == null)
            {
                throw new ParseException(reference.ParseLocation, $"Attribute '{reference.Name}' not found from class model '{classModel.FullyQualifiedName}'.");
            }

            reference.Target = attribute;
        }
        
        public static void Resolve(this ClassRelationReference reference, IClassModel classModel)
        {
            if (reference.Target != null)
            {
                return;
            }
            
            var relation = classModel.OutgoingRelations.FirstOrDefault(x => x.Name == reference.Name);
                
            if (relation == null)
            {
                throw new ParseException(reference.ParseLocation, $"Relation '{reference.Name}' not found from class model '{classModel.FullyQualifiedName}'.");
            }

            reference.Target = relation;
        }
        
        public static void Resolve(this ClassAttributeReference reference, IClassRelation relation)
        {
            if (reference.Target != null)
            {
                return;
            }
            
            var attribute = relation.Attributes.FirstOrDefault(x => x.Name == reference.Name);
                
            if (attribute == null)
            {
                throw new ParseException(reference.ParseLocation, $"Attribute '{reference.Name}' not found from relation '{relation.Name}'.");
            }

            reference.Target = attribute;
        }

    }
}