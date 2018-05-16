using Polygen.Core.DesignModel;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.Entity.Parser
{
    /// <summary>
    /// Entity design model parser.
    /// </summary>
    public class EntityParser: DesignModelGeneratorBase
    {
        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            return new Entity(xmlElement.GetStringAttributeValue("name"), context.Namespace, xmlElement);
        }
    }
}
