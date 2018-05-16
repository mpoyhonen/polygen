using Polygen.Core.DesignModel;
using Polygen.Core.OutputModel;
using Polygen.Core.Project;
using Polygen.Core.Stage;
using Polygen.Plugins.NHibernate.Output.Backend;

namespace Polygen.Plugins.NHibernate.StageHandler
{
    /// <summary>
    /// Creates output mopdels from the desing models.
    /// </summary>
    public class CreateOutputModels: StageHandlerBase
    {
        public CreateOutputModels(): base(StageType.GenerateOutputModels, "Entity", "Base")
        {
        }

        public IDesignModelCollection DesignModels { get; set; }
        public IProjectCollection Projects { get; set; }
        public IOutputModelCollection OutputModels { get; set; }
        public EntityConverter EntityConverter { get; set; }

        public override void Execute()
        {
            foreach (var entityDesignModel in DesignModels.RootNamespace.FindDesignModelsByType("Entity"))
            {
                var entity = (Base.Models.Entity.Entity)entityDesignModel;

                //this.OutputModels.AddOutputModel(this.EntityConverter.CreateEntityGeneratedClass(entity, BasePluginConstants.Language_CSharp));
                //this.OutputModels.AddOutputModel(this.EntityConverter.CreateEntityCustomClass(entity, BasePluginConstants.Language_CSharp));
            }
        }
    }
}
