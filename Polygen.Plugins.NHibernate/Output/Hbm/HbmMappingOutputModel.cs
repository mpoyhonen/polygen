using System.Xml.Linq;
using Polygen.Common.Xml;
using Polygen.Core.Project;
using Polygen.Plugins.Base;

namespace Polygen.Plugins.NHibernate.Output.Hbm
{
    public class HbmMappingOutputModel : XmlOutputModel
    {
        public HbmMappingOutputModel(XElement element, IProjectFile file = null) 
            : base(element, null, file, NHibernatePluginConstants.OutputModelType_Mapping)
        {
            Renderer = new XmlOutputModelRenderer();
        }
    }
}
