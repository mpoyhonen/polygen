using System;
using System.Collections.Generic;
using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;
using Polygen.Core.Schema;

namespace Polygen.Core.Impl.Parser
{
    public class DesignModelGeneratorFactory : IDesignModelGeneratorFactory
    {
        private readonly Dictionary<object, IDesignModelGenerator> _map = new Dictionary<object, IDesignModelGenerator>();

        public IDesignModelGenerator GetGenerator(ISchemaElement schemaElement)
        {
            _map.TryGetValue(schemaElement, out var generator);

            return generator;
        }

        public void RegisterFactory(ISchemaElement schemaElement, IDesignModelGenerator generator)
        {
            if (_map.ContainsKey(schemaElement))
            {
                throw new ConfigurationException($"Design model generator already registered for schema element '{schemaElement.Name.LocalName}'.");
            }

            _map[schemaElement] = generator;
        }

        public void RegisterFactory(ISchemaElement schemaElement,
            Func<IXmlElement, DesignModelParseContext, IDesignModel> generatorFn, string id)
        {
            RegisterFactory(schemaElement, new FuncConverterImpl(generatorFn));
        }

        internal class FuncConverterImpl : IDesignModelGenerator
        {
            private readonly Func<IXmlElement, DesignModelParseContext, IDesignModel> _fn;

            internal FuncConverterImpl(Func<IXmlElement, DesignModelParseContext, IDesignModel> fn)
            {
                _fn = fn;
            }

            public IDesignModel GenerateDesignModel(IXmlElement xmlElement, DesignModelParseContext context)
            {
                return _fn(xmlElement, context);
            }
        }
    }
}
