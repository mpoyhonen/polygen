using Polygen.Core.DesignModel;
using Polygen.Core.OutputModel;
using Polygen.Core.Stage;

namespace Polygen.Core.Impl.StageHandler
{
    /// <summary>
    /// Generates output models from the desing models.
    /// </summary>
    public class GenerateOutputModels: StageHandlerBase
    {
        public GenerateOutputModels(): base(StageType.GenerateOutputModels, "Core")
        {
        }

        public IDesignModelCollection DesignModels { get; set; }
        public IOutputModelCollection OutputModels { get; set; }

        public override void Execute()
        {
            foreach (var designModel in this.DesignModels.GetAllDesignModels())
            {
                foreach (var targetPlatform in designModel.OutputConfiguration.GetTargetPlatformsForDesignModel(designModel))
                {
                    var generator = targetPlatform.GetOutputModelGenerator(designModel.Type);

                    foreach (var outputModel in generator.GenerateOutputModels(designModel))
                    {
                        this.OutputModels.AddOutputModel(outputModel);
                    }
                }
            }
        }
    }
}
