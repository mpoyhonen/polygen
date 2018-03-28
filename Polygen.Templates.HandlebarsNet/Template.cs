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
            this.Name = name;
            this._handlebars = handlebars;
            this.Source = source;
        }

        public string Name { get; }
        public string Type { get; } = Constants.TemplateType;
        public TemplateSource Source { get; }

        public string GetTemplateText()
        {
            if (this._templateText == null)
            {
                if (this.Source.IsFile)
                {
                    this._templateText = File.ReadAllText(this.Source.FilePath, Encoding.UTF8);
                }
                else
                {
                    this._templateText = this.Source.Text;
                }
            }

            return this._templateText;
        }

        public void Render(Dictionary<string, object> data, TextWriter writer)
        {
            this.GetRenderFn()(writer, data);
        }

        public Action<TextWriter, object> GetRenderFn()
        {
            if (this._compiledTemplateInstance == null)
            {
                using (var reader = new StringReader(this.GetTemplateText()))
                {
                    this._compiledTemplateInstance = this._handlebars.Compile(reader);
                }
            }

            return this._compiledTemplateInstance;
        }
    }
}
