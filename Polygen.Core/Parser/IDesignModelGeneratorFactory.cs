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
    /// Contains all registered design model generators.
    /// </summary>
    public interface IDesignModelGeneratorFactory
    {
        /// <summary>
        /// Registers a new design model generator for the given design model element.
        /// </summary>
        /// <param name="schemaElement"></param>
        /// <param name="generator"></param>
        void RegisterFactory(ISchemaElement schemaElement, IDesignModelGenerator generator);

        /// <summary>
        /// Registers a new design model IDesignModelGenerator function for the given XML element.
        /// </summary>
        /// <param name="schemaElement"></param>
        /// <param name="generatorFn"></param>
        /// <param name="id"></param>
        void RegisterFactory(ISchemaElement schemaElement, Func<IXmlElement, DesignModelParseContext, IDesignModel> generatorFn, string id);
        /// <summary>
        /// Returns a design model generator registered for the given schema element.
        /// </summary>
        /// <param name="schemaElement"></param>
        /// <returns></returns>
        IDesignModelGenerator GetGenerator(ISchemaElement schemaElement);
    }
}
