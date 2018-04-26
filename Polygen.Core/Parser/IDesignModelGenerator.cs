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
        /// Generator ID. Used by other generators to define dependencies on this generator.
        /// </summary>
        string Id { get; }
        /// <summary>
        /// IDs of generators which much be executed before this one.
        /// </summary>
        IEnumerable<string> Dependencies { get; }
        /// <summary>
        /// Generates a new design model instance for the given XML element.
        /// </summary>
        /// <param name="xmlElement"></param>
        /// <param name="context"></param>
        /// <returns>Parsed design model. Can be null.</returns>
        IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context);
    }

    /// <summary>
    /// Base class for a design model generator.
    /// </summary>
    public abstract class DesignModelGeneratorBase : IDesignModelGenerator
    {
        public DesignModelGeneratorBase(string id, IEnumerable<string> dependencies = null)
        {
            Id = id;
            Dependencies = dependencies ?? Enumerable.Empty<string>();
        }

        /// <inheritdoc />
        public string Id { get; }

        /// <inheritdoc />
        public IEnumerable<string> Dependencies { get; }

        /// <inheritdoc />
        public abstract IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context);
    }
}
