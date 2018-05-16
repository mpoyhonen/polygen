using System.Collections.Generic;
using System.Linq;
using Polygen.Core.ClassModel;
using Polygen.Core.DesignModel;
using Polygen.Core.Stage;
using Polygen.Core.Utils;

namespace Polygen.Plugins.Base.Models.ClassModelDerivation.StageHandler
{
    /// <summary>
    /// Stage handler for processing all class derivations (DeriveFrom element data). This is called after
    /// all desing models have been parsed at their attributes and relations have
    /// been defined.
    /// </summary>
    public class ProcessClassDerivations: StageHandlerBase
    {
        public ProcessClassDerivations(): base(StageType.AfterParseDesignModels, nameof(ProcessClassDerivations))
        {
        }

        public IDesignModelCollection DesignModelCollection { get; set; }

        public override void Execute()
        {
            // Find all design models which have implement IClassDesignModel interface.
            var classModels = DesignModelCollection
                .GetAllDesignModels()
                .Where(x => x is IClassModel)
                .Cast<IClassModel>()
                .Where(x => x.GetClassDerivationData() != null);
            
            // Sort the derivations according to the source, so we can handle nested derivations.
            var dependecyMap = new DependencyMap<IClassModel>();
            var sourceEntries = new List<IClassModel>();

            foreach (var destinationClassModel in classModels)
            {
                var data = destinationClassModel.GetClassDerivationData();
                var sourceClassModel = data.Source.Target;
                
                var sourceId = $"{sourceClassModel.Type}:{sourceClassModel.FullyQualifiedName}";
                var destinationId = $"{destinationClassModel.Type}:{destinationClassModel.FullyQualifiedName}";

                sourceEntries.Add(sourceClassModel);
                dependecyMap.Add(destinationClassModel, destinationId, new[] { sourceId });
            }
            
            // Add any missing source models.
            foreach (var sourceClassModel in sourceEntries)
            {
                var sourceId = $"{sourceClassModel.Type}:{sourceClassModel.FullyQualifiedName}";

                if (!dependecyMap.ContainsId(sourceId))
                {
                    dependecyMap.Add(sourceClassModel, sourceId);
                }
            }
            
            // Process the models in the right order.
            foreach (var destinationClassModel in dependecyMap.Entries.Where(x => x.GetClassDerivationData() != null))
            {
                // Resolve source and destination model references.
                var data = destinationClassModel.GetClassDerivationData();
                
                data.Source.Resolve(DesignModelCollection);
                data.Destination.Resolve(DesignModelCollection);

                var sourceModel = data.Source.Target;
                var destinationModel = data.Destination.Target;
                
                // Copy properties from the source model. This will copy the actual XML attributes used for the design model element.
                destinationModel.CopyPropertiesFrom(sourceModel, data.ParseLocation);
                
                if (data.CopyAttributes)
                {
                    var existing = new Dictionary<string, IClassAttribute>();

                    foreach (var attribute in destinationModel.Attributes)
                    {
                        existing[attribute.Name] = attribute;
                    }
                    
                    foreach (var sourceAttribute in data.Source.Target.Attributes.Where(x => !data.SkippedAttributeNames.Contains(x.Name)))
                    {
                        if (!existing.ContainsKey(sourceAttribute.Name))
                        {
                            destinationModel.CloneAttribute(sourceAttribute, data.ParseLocation);
                        }
                    }
                }
                
                if (data.CopyRelations)
                {
                    var existing = new Dictionary<string, IClassRelation>();

                    foreach (var relation in destinationModel.OutgoingRelations)
                    {
                        existing[relation.Name] = relation;
                    }
                    
                    foreach (var sourceRelation in sourceModel.OutgoingRelations.Where(x => !data.SkippedRelationNames.Contains(x.Name)))
                    {
                        if (existing.ContainsKey(sourceRelation.Name))
                        {
                            continue;
                        }

                        // Find out the relation destination model. There needs to exists a model
                        // which has the same type the new models and fully qualified name as the source relation destination class model.
                        var reference = new ClassModelReference(sourceRelation.Destination.FullyQualifiedName, sourceRelation.Destination.Namespace, destinationModel.Type, data.ParseLocation);
                        var relationDestinationClassModel = reference.Namespace.GetDesignModel(reference, true);
                            
                        // Create the new relation.
                        var newRelation = destinationModel.CloneRelation(sourceRelation, relationDestinationClassModel, data.ParseLocation);
                        
                        // Copy relation attributes.
                        foreach (var attribute in sourceRelation.Attributes)
                        {
                            newRelation.CloneAttribute(attribute, data.ParseLocation);
                        }
                    }
                }
            }
        }
    }
}
