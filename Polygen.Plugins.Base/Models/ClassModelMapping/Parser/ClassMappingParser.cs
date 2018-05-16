using Polygen.Core.ClassModel;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.ClassModelMapping.Parser
{
    /// <summary>
    /// Parser for model mapping configuration.
    /// </summary>
    public class ClassMappingParser: DesignModelGeneratorBase
    {
        private readonly IDesignModelCollection _designModelCollection;

        public ClassMappingParser(IDesignModelCollection designModelCollection)
        {
            _designModelCollection = designModelCollection;
        }

        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            var sourceDesignModelName = xmlElement.GetStringAttributeValue("src");
            var sourceDesignModelNameSet = !string.IsNullOrWhiteSpace(sourceDesignModelName);
            var destinationDesignModelName = xmlElement.GetStringAttributeValue("dest");
            var destinationDesignModelNameSet = !string.IsNullOrWhiteSpace(destinationDesignModelName);

            if (sourceDesignModelNameSet && destinationDesignModelNameSet)
            {
                throw new ParseException(xmlElement.ParseLocation, $"Both 'src' and 'dest' attributes cannot be set.");
            }
            
            if (!sourceDesignModelNameSet && !destinationDesignModelNameSet)
            {
                throw new ParseException(xmlElement.ParseLocation, $"Either 'src' or 'dest' attribute must be set.");
            }

            var currentDesignModel = context.DesignModel;

            if (!(currentDesignModel is IClassModel currentClassModel))
            {
                throw new ParseException(xmlElement.ParseLocation, $"Mapping model '{currentDesignModel.FullyQualifiedName}' must be a class-like design model.");
            }

            var ns = currentDesignModel.Namespace;
            var sourceReference = sourceDesignModelNameSet 
                ? _designModelCollection.CreateClassModelReference(ns, xmlElement.ParseLocation, sourceDesignModelName)
                : currentClassModel.CreateClassModelReference(xmlElement.ParseLocation);
            var destinationReference = destinationDesignModelNameSet 
                ? _designModelCollection.CreateClassModelReference(ns, xmlElement.ParseLocation, destinationDesignModelName)
                : currentClassModel.CreateClassModelReference(xmlElement.ParseLocation);

            var classMapping = new ClassMapping
            {
                Name = xmlElement.GetStringAttributeValue("name", null),
                Source = sourceReference,
                Destination = destinationReference,
                MapAttributes = xmlElement.GetBoolAttributeValue("attributes", true),
                MapRelations = xmlElement.GetBoolAttributeValue("relations", true),
                AddMissing = xmlElement.GetBoolAttributeValue("add-missing", true),
                TwoWay = xmlElement.GetBoolAttributeValue("two-way", true),
                ParseLocation = xmlElement.ParseLocation
            };

            foreach (var attributeElement in xmlElement.GetChildElments("Attribute"))
            {
                var mapping = new AttributeMapping
                {
                    Source = new ClassAttributeReference(attributeElement.GetStringAttributeValue("src"), attributeElement.ParseLocation),
                    Destination = new ClassAttributeReference(attributeElement.GetStringAttributeValue("dest"), attributeElement.ParseLocation),
                    ParseLocation = attributeElement.ParseLocation
                };
                
                classMapping.AttributeMappings.Add(mapping);
            }
            
            foreach (var relationElement in xmlElement.GetChildElments("Relation"))
            {
                var relationMapping = new RelationMapping
                {
                    Source = new ClassRelationReference(relationElement.GetStringAttributeValue("src"), relationElement.ParseLocation),
                    Destination = new ClassRelationReference(relationElement.GetStringAttributeValue("dest"), relationElement.ParseLocation),
                    MapAttributes = relationElement.GetBoolAttributeValue("attributes", true),
                    AddMissing = relationElement.GetBoolAttributeValue("add-missing", true),
                    TwoWay = relationElement.GetBoolAttributeValue("two-way", true),
                    ParseLocation = relationElement.ParseLocation
                };
                
                foreach (var attributeElement in relationElement.GetChildElments("Attribute"))
                {
                    var relationAttributeMapping = new AttributeMapping
                    {
                        Source = new ClassAttributeReference(attributeElement.GetStringAttributeValue("src"), attributeElement.ParseLocation),
                        Destination = new ClassAttributeReference(attributeElement.GetStringAttributeValue("dest"), attributeElement.ParseLocation),
                        ParseLocation = attributeElement.ParseLocation
                    };
                
                    relationMapping.AttributeMappings.Add(relationAttributeMapping);
                }                
                
                classMapping.RelationMappings.Add(relationMapping);
            }

            currentClassModel.SetClassMappingData(classMapping);
            
            return null;
        }
    }
}
