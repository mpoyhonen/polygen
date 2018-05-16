using System.Linq;
using Polygen.Core.ClassModel;
using Polygen.Core.DesignModel;
using Polygen.Core.Stage;

namespace Polygen.Plugins.Base.Models.ClassModel.StageHandler
{
    /// <summary>
    /// Stage handler for resolving all destination class models in class model references.
    /// </summary>
    public class ResolveReferences: StageHandlerBase
    {
        public ResolveReferences(): base(StageType.ResolveDesignModelReferences, nameof(ResolveReferences))
        {
        }

        public IDesignModelCollection DesignModelCollection { get; set; }

        public override void Execute()
        {
            var classModels = DesignModelCollection
                .GetAllDesignModels()
                .Where(x => x is Core.Impl.ClassModel.ClassModel)
                .Cast<Core.Impl.ClassModel.ClassModel>();

            foreach (var classModel in classModels)
            {
                classModel.ResolveReferences(DesignModelCollection);
            }
        }
    }
}
