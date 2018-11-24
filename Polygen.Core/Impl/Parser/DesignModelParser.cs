using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using System.Collections.Generic;
using Polygen.Core.Schema;

namespace Polygen.Core.Impl.Parser
{
    public class DesignModelParser : IDesignModelParser
    {
        private readonly IDesignModelGeneratorFactory _designModelConverterFactory;

        public DesignModelParser(IDesignModelGeneratorFactory designModelConverterFactory)
        {
            _designModelConverterFactory = designModelConverterFactory;
        }

        public void Parse(IXmlElement rootElement, IDesignModelCollection collection)
        {
            var context = new DesignModelParseContext(collection)
            {
                Namespace = collection.RootNamespace,
                XmlElement = rootElement
            };

            ParseDesignModelElements(context);
        }

        private void ParseDesignModelElements(DesignModelParseContext context)
        {
            var designModelConverter = _designModelConverterFactory.GetGenerator(context.XmlElement.Definition);
            var designModel = designModelConverter?.GenerateDesignModel(context.XmlElement, context);

            if (designModel != null)
            {
                context.XmlElement.DesignModel = designModel;
                context.DesignModel = designModel;
                context.Namespace.AddDesignModel(designModel);
                context.Collection.AddDesignModel(designModel);

                foreach (var attribute in context.XmlElement.Attributes)
                {
                    ParseProperty(designModel, attribute);
                }
            }

            foreach (var childElement in context.XmlElement.Children)
            {
                var childContext = new DesignModelParseContext(context)
                {
                    XmlElement = childElement
                };

                ParseDesignModelElements(childContext);
            }
        }

        private static void ParseProperty(IDesignModel designModel, IXmlElementAttribute attribute)
        {
            var definition = attribute.Definition;
            var name = definition.Name.LocalName;

            if (designModel.GetProperty(name) != null)
            {
                // Property is already defined, so skip attribute.
                return;
            }
            
            var value = attribute.Value;
            var property = new DesignModelProperty(definition.Name.LocalName, definition.Type, value, definition, attribute.ParseLocation);
            
            designModel.AddProperty(property);
        }
    }
}
