using Polygen.Core.DesignModel;
using Polygen.Core.OutputModel;
using Polygen.Core.Stage;

namespace Polygen.Core.Impl.StageHandler
{
    /// <summary>
    /// Generates output models from the design models.
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
            foreach (var designModel in DesignModels.GetAllDesignModels())
            {
                foreach (var targetPlatform in designModel.OutputConfiguration.GetTargetPlatformsForDesignModel(designModel))
                {
                    var generator = targetPlatform.GetOutputModelGenerator(designModel.DesignModelType);

                    foreach (var outputModel in generator.GenerateOutputModels(designModel))
                    {
                        OutputModels.AddOutputModel(outputModel);
                    }
                }
            }
        }
    }
}
