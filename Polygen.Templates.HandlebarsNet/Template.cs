using Polygen.Core.Template;
using HandlebarsDotNet;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Polygen.Core.Exceptions;

namespace Polygen.Templates.HandlebarsNet
{
    public class Template : ITemplate
    {
        private readonly IHandlebars _handlebars;
        private string _templateText;
        private Action<TextWriter, object> _compiledTemplateInstance;

        public Template(string name, IHandlebars handlebars, TemplateSource source)
        {
            Name = name;
            _handlebars = handlebars;
            Source = source;
        }

        public string Name { get; }
        public string Type { get; } = Constants.TemplateType;
        public TemplateSource Source { get; }

        public string GetTemplateText()
        {
            if (_templateText == null)
            {
                if (Source.IsFile)
                {
                    _templateText = File.ReadAllText(Source.FilePath, Encoding.UTF8);
                }
                else
                {
                    _templateText = Source.Text;
                }
            }

            return _templateText;
        }

        public void Render(Dictionary<string, object> data, TextWriter writer)
        {
            GetRenderFn()(writer, data);
        }

        public Action<TextWriter, object> GetRenderFn()
        {
            if (_compiledTemplateInstance == null)
            {
                using (var reader = new StringReader(GetTemplateText()))
                {
                    _compiledTemplateInstance = _handlebars.Compile(reader);
                }
            }

            return _compiledTemplateInstance;
        }
    }
}
