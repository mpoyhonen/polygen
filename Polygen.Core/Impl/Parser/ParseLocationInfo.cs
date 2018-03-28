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
            this.Element = element;
            this.LineInfo = element;
            this.File = file;
        }

        public ParseLocationInfo(XAttribute attribute, IProjectFile file)
        {
            this.Attribute = attribute;
            this.LineInfo = attribute;
            this.File = file;
        }

        public XElement Element { get; }
        public XAttribute Attribute { get; }
        public IXmlLineInfo LineInfo { get; }
        public IProjectFile File { get; }

        public override string ToString()
        {
            var buf = new StringBuilder(512);

            buf.Append("file: ");
            buf.Append(this.File.GetSourcePath(false) ?? this.File.RelativePath);

            if (this.LineInfo.HasLineInfo())
            {
                buf.Append(" , line: ").Append(this.LineInfo.LineNumber).Append(", column: ").Append(this.LineInfo.LinePosition);
            }

            return buf.ToString();
        }
    }
}
