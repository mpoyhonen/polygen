using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using Polygen.Core.Schema;

namespace Polygen.Core.Parser
{
    /// <summary>
    /// Interface for generating desing model from a parsed XML element.
    /// </summary>
    public interface IDesignModelGenerator
    {
        /// <summary>
        /// Generates a new design model instance for the given XML element.
        /// </summary>
        /// <param name="designModel"></param>
        /// <param name="context"></param>
        /// <returns>Parsed design model. Can be null.</returns>
        IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context);
    }
}
