using Polygen.Core.DesignModel;
using Polygen.Core.Parser;
using System.Collections.Generic;

namespace Polygen.Core.Impl.Parser
{
    public class DesignModelParser : IDesignModelParser
    {
        private readonly IDesignModelGeneratorFactory _designModelConverterFactory;

        public DesignModelParser(IDesignModelGeneratorFactory designModelConverterFactory)
        {
            this._designModelConverterFactory = designModelConverterFactory;
        }

        public void Parse(IXmlElement rootElement, IDesignModelCollection collection)
        {
            var context = new DesignModelParseContext(collection)
            {
                Namespace = collection.RootNamespace,
                XmlElement = rootElement
            };

            this.ParseDesignModelElements(context);
        }

        private void ParseDesignModelElements(DesignModelParseContext context)
        {
            var designModelConverter = this._designModelConverterFactory.GetGenerator(context.XmlElement.Definition);
            var designModel = designModelConverter?.GenerateDesignModel(context.XmlElement, context);

            if (designModel != null)
            {
                context.XmlElement.DesignModel = designModel;
                context.DesignModel = designModel;
                context.Namespace.AddDesignModel(designModel);
                context.Collection.AddDesignModel(designModel);
            }

            foreach (var childElement in context.XmlElement.Children)
            {
                var childContext = new DesignModelParseContext(context)
                {
                    XmlElement = childElement
                };

                this.ParseDesignModelElements(childContext);
            }
        }
    }
}
