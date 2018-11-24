using System.Collections.Generic;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;
using Polygen.Core.Stage;

namespace Polygen.Plugins.Base.StageHandler
{
    /// <summary>
    /// Parses loaded schema elements into desing models.
    /// </summary>
    public class ParseDesignModels : StageHandlerBase
    {
        public ParseDesignModels(): base(StageType.ParseDesignModels, "Base")
        {
        }

        public IDesignModelParser DesignModelParser { get; set; }
        public IDesignModelCollection DesignModels { get; set; }
        public DesignModelParseState ParseState { get; set; }

        public override void Execute()
        {
            if (ParseState?.Elements == null)
            {
                throw new ConfigurationException("No elements have been parsed.");
            }

            foreach (var xmlElement in ParseState.Elements)
            {
                DesignModelParser.Parse(xmlElement, DesignModels);
            }

            ParseState.Elements = null;
        }
    }

    /// <summary>
    /// Class to keep design model parsing state between handlers.
    /// </summary>
    public class DesignModelParseState
    {
        public List<IXmlElement> Elements { get; set; }
    }
}
