using System.Xml;
using Polygen.Core.Project;

namespace Polygen.Core.Parser
{
    public interface IParseLocationInfo
    {
        IXmlLineInfo LineInfo { get; }
        IProjectFile File { get; }
    }
}
