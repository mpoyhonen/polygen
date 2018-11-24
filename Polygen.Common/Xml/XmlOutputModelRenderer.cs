using System.IO;
using System.Text;
using System.Xml;
using Polygen.Core.Exceptions;
using Polygen.Core.OutputModel;
using Polygen.Core.Renderer;

namespace Polygen.Common.Xml
{
    public class XmlOutputModelRenderer : IOutputModelRenderer
    {
        public void Render(IOutputModel outputModel, TextWriter writer)
        {
            var xmlOutputModel = outputModel as XmlOutputModel;

            if (xmlOutputModel == null)
            {
                throw new RenderException("Output model must be an XmlOutputModel.");
            }

            if (xmlOutputModel.Element == null)
            {
                throw new RenderException("Output model root element not set.");
            }

            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                CloseOutput = false,
                Indent = true,
                OmitXmlDeclaration = true
            };

            using (var xmlWriter = XmlWriter.Create(writer, settings))
            {
                xmlOutputModel.Element.WriteTo(xmlWriter);
            }
        }
    }
}
