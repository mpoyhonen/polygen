using Polygen.Common.Xml;
using Polygen.Core.Project;
using System.Xml.Linq;

namespace Polygen.Plugins.Base.Output.Xsd
{
    public class SchemaXsdOutputModel : XmlOutputModel
    {
        public SchemaXsdOutputModel(XElement element, IProjectFile file = null) : base(element, null, file, BasePluginConstants.OutputModelName_SchemaXSD)
        {
            this.Renderer = new XmlOutputModelRenderer();
        }
    }
}
