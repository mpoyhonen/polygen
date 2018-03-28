using System.IO;
using Polygen.Core.DesignModel;
using Polygen.Core.Project;
using Polygen.Core.Schema;

namespace Polygen.Core.Parser
{
    /// <summary>
    /// Interface for parsing design model XML file.
    /// </summary>
    public interface IXmlElementParser
    {
        /// <summary>
        /// Parses the XML structure into IXmlElement objects.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="schema"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        IXmlElement Parse(TextReader reader, ISchema schema, IProjectFile file);
    }
}
