using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Parser
{
    /// <summary>
    /// Entity design model parser.
    /// </summary>
    public class EntityParser: DesignModelGeneratorBase
    {
        public EntityParser() : base(nameof(Entity))
        {
        }

        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            return new Entity(context.Namespace, xmlElement);
        }
    }
}
