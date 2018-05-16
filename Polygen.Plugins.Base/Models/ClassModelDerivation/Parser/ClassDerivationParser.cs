using Polygen.Core.ClassModel;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;
using Polygen.Core.Reference;

namespace Polygen.Plugins.Base.Models.ClassModelDerivation.Parser
{
    /// <summary>
    /// Parser for class model derivation configuration.
    /// </summary>
    public class ClassDerivationParser: DesignModelGeneratorBase
    {
        private readonly IDesignModelCollection _designModelCollection;

        public ClassDerivationParser(IDesignModelCollection designModelCollection)
        {
            _designModelCollection = designModelCollection;
        }

        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            var sourceDesignModelName = xmlElement.GetStringAttributeValue("src");
            var destinationDesingModel = context.DesignModel;

            if (!(destinationDesingModel is IClassModel destinationClassDesignModel))
            {
                throw new ParseException(xmlElement.ParseLocation, $"Mapping destination model '{destinationDesingModel.FullyQualifiedName}' must be a class-like design model.");
            }

            var ns = destinationDesingModel.Namespace;
            var classData = new ClassDerivation
            {
                Source = _designModelCollection.CreateClassModelReference(ns, xmlElement.ParseLocation, sourceDesignModelName),
                Destination = destinationClassDesignModel.CreateClassModelReference(xmlElement.ParseLocation),           
                ParseLocation = xmlElement.ParseLocation
            };

            foreach (var attributeElement in xmlElement.GetChildElments("Attribute"))
            {
                var attributeData = new AttributeDerivation
                {
                    Source = new ClassAttributeReference(attributeElement.GetStringAttributeValue("src"), attributeElement.ParseLocation),
                    Destination = new ClassAttributeReference(attributeElement.GetStringAttributeValue("dest"), attributeElement.ParseLocation),
                    ParseLocation = attributeElement.ParseLocation
                };
                
                classData.AttributeDerivations.Add(attributeData);
            }
            
            foreach (var relationElement in xmlElement.GetChildElments("Relation"))
            {
                var relationData = new RelationDerivation
                {
                    Source = new ClassRelationReference(relationElement.GetStringAttributeValue("src"), relationElement.ParseLocation),
                    Destination = new ClassRelationReference(relationElement.GetStringAttributeValue("dest"), relationElement.ParseLocation),
                    MapAttributes = relationElement.GetBoolAttributeValue("attributes", true),
                    ParseLocation = relationElement.ParseLocation
                };
                
                foreach (var attributeElement in relationElement.GetChildElments("Attribute"))
                {
                    var relationAttributeData = new AttributeDerivation
                    {
                        Source = new ClassAttributeReference(attributeElement.GetStringAttributeValue("src"), attributeElement.ParseLocation),
                        Destination = new ClassAttributeReference(attributeElement.GetStringAttributeValue("dest"), attributeElement.ParseLocation),
                        ParseLocation = attributeElement.ParseLocation
                    };
                
                    relationData.AttributeDerivations.Add(relationAttributeData);
                }                
                
                classData.RelationDerivations.Add(relationData);
            }

            destinationClassDesignModel.SetClassDerivationData(classData);
            
            return null;
        }
    }
}
