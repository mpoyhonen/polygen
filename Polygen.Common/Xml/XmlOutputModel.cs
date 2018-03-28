using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Polygen.Core.DesignModel;
using Polygen.Core.OutputModel;
using Polygen.Core.Project;

namespace Polygen.Common.Xml
{
    public class XmlOutputModel : Core.Impl.OutputModel.OutputModelBase
    {
        public XmlOutputModel(XElement element, INamespace ns, IProjectFile file = null, string type = null): base(type, ns, file: file)
        {
            this.Element = element;
            this.Type = type ?? Constants.OutputModelType_Xml;
        }

        public XElement Element { get; set; }
    }
}
