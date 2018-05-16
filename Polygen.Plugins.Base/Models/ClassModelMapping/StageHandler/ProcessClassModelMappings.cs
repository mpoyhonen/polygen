using System.Linq;
using Polygen.Core.ClassModel;
using Polygen.Core.DesignModel;
using Polygen.Core.Stage;

namespace Polygen.Plugins.Base.Models.ClassModelMapping.StageHandler
{
    /// <summary>
    /// Stage handler for processing all class model mappings. This is called after
    /// all desing models have been parsed at their attributes and relations have
    /// been defined.
    /// </summary>
    public class ProcessClassModelMappings: StageHandlerBase
    {
        public ProcessClassModelMappings(): base(StageType.AfterParseDesignModels, nameof(ProcessClassModelMappings))
        {
        }

        public IDesignModelCollection DesignModelCollection { get; set; }

        public override void Execute()
        {
            // Find all design models which have implement IClassModel interface.
            var classDesignModels = DesignModelCollection
                .GetAllDesignModels()
                .Where(x => x is IClassModel)
                .Cast<IClassModel>()
                .Where(x => x.GetClassMappingData() != null)
                .Where(x => x.GetClassMappingData().AddMissing);

            foreach (var destinationClassDesignModel in classDesignModels)
            {
                var mapping = destinationClassDesignModel.GetClassMappingData();
                
                mapping.Source.Resolve(DesignModelCollection);
                mapping.Destination.Resolve(DesignModelCollection);
                
                foreach (var attributeMapping in mapping.AttributeMappings)
                {
                    attributeMapping.Source.Resolve(mapping.Source.Target);
                    attributeMapping.Destination.Resolve(mapping.Destination.Target);
                }
                
                foreach (var relationMapping in mapping.RelationMappings)
                {
                    relationMapping.Source.Resolve(mapping.Source.Target);
                    relationMapping.Destination.Resolve(mapping.Destination.Target);
                    
                    foreach (var attributeMapping in relationMapping.AttributeMappings)
                    {
                        attributeMapping.Source.Resolve(relationMapping.Source.Target);
                        attributeMapping.Destination.Resolve(relationMapping.Source.Target);
                    }
                }
            }
        }
    }
}
