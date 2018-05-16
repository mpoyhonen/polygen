using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;

namespace Polygen.Plugins.Base.Models.Namespace.Parser
{
    /// <summary>
    /// Namespace design model parser.
    /// </summary>
    public class NamespaceParser: DesignModelGeneratorBase
    {
        public override IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
        {
            var namespaceName = xmlElement.GetAttribute("name")?.Value;

            if (string.IsNullOrWhiteSpace(namespaceName))
            {
                throw new DesignModelException(context.DesignModel, "Namespace name not set");
            }

            var ns = context.Collection.DefineNamespace(namespaceName);

            context.Namespace = ns;

            return null;
        }
    }
}
