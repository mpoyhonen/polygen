using System.Text;
using System.Xml;
using System.Xml.Linq;
using Polygen.Core.Parser;
using Polygen.Core.Project;

namespace Polygen.Core.Impl.Parser
{
    public class ParseLocationInfo: IParseLocationInfo
    {
        public ParseLocationInfo(XElement element, IProjectFile file)
        {
            Element = element;
            LineInfo = element;
            File = file;
        }

        public ParseLocationInfo(XAttribute attribute, IProjectFile file)
        {
            Attribute = attribute;
            LineInfo = attribute;
            File = file;
        }

        public XElement Element { get; }
        public XAttribute Attribute { get; }
        public IXmlLineInfo LineInfo { get; }
        public IProjectFile File { get; }

        public override string ToString()
        {
            var buf = new StringBuilder(512);

            buf.Append("file: ");
            buf.Append(File.GetSourcePath(false) ?? File.RelativePath);

            if (LineInfo.HasLineInfo())
            {
                buf.Append(" , line: ").Append(LineInfo.LineNumber).Append(", column: ").Append(LineInfo.LinePosition);
            }

            return buf.ToString();
        }
    }
}
