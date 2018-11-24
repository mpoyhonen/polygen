using System.Xml.Linq;
using Polygen.Common.Xml;
using Polygen.Core.Project;

namespace Polygen.Plugins.Base.Output.Xsd
{
    public class XsdOutputModel : XmlOutputModel
    {
        public XsdOutputModel(XElement element, IProjectFile file = null) : base(element, null, file, BasePluginConstants.OutputModelName_SchemaXSD)
        {
            Renderer = new XmlOutputModelRenderer();
        }
    }
}
