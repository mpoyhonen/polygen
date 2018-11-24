using System.Collections.Generic;
using System.Linq;
using Polygen.Core.Parser;
using Polygen.Core.Project;
using Polygen.Core.Schema;
using Polygen.Core.Stage;

namespace Polygen.Plugins.Base.StageHandler
{
    /// <summary>
    /// Parses design model files into schema elements.
    /// </summary>
    public class ParseDesignModelFiles : StageHandlerBase
    {
        public ParseDesignModelFiles(): base(StageType.ParseInputXmlFiles, "Base")
        {
        }

        public IProjectCollection ProjectCollection { get; set; }
        public ISchemaCollection Schemas { get; set; } 
        public IXmlElementParser XmlElementParser { get; set; }
        public DesignModelParseState ParseState { get; set; }

        public override void Execute()
        {
            var inputXmlFiles = ProjectCollection
                 .Projects
                 .SelectMany(x => x.DesignModelFiles)
                 .ToList();

            var schema = Schemas.GetSchemaByNamespace(BasePluginConstants.DesignModel_SchemaNamespace);

            ParseState.Elements = new List<IXmlElement>();

            foreach (var inputXmlFile in inputXmlFiles)
            {
                using (var reader = inputXmlFile.OpenAsTextForReading())
                {
                    ParseState.Elements.Add(XmlElementParser.Parse(reader, schema, inputXmlFile));
                }
            }
        }
    }
}
