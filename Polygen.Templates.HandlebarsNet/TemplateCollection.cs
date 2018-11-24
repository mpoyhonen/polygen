using Polygen.Core.Exceptions;
using Polygen.Core.Template;
using Polygen.Core.Utils;
using HandlebarsDotNet;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;
using System.Text.RegularExpressions;
using System.Text;

namespace Polygen.Templates.HandlebarsNet
{
    public class TemplateCollection : ITemplateCollection
    {
        private readonly Dictionary<string, Template> _templateMap = new Dictionary<string, Template>();

        public TemplateCollection()
        {
            var config = new HandlebarsConfiguration
            {
                // Not yet available. Uncomment later.
                //ThrowOnUnresolvedBindingExpression = true,
                TextEncoder = new NoopTextEncoder()
            };

            Instance = Handlebars.Create(config);
        }

        public IHandlebars Instance { get; }

        public ITemplate GetTemplate(string name, bool throwIfMissing = true)
        {
            if (!_templateMap.TryGetValue(name, out var template) && throwIfMissing)
            {
                throw new ConfigurationException($"Template '{name}' is not registered.");
            }

            return template;
        }

        public void AddTemplate(string name, string templateText, bool overrideExisting = false)
        {
            var template = new Template(name, Instance, TemplateSource.CreateForText(templateText));

            RegisterTemplate(template, overrideExisting);
        }

        public void LoadTemplates(params string[] folders)
        {
            foreach (var folder in folders)
            {
                foreach (var file in Directory.EnumerateFiles(folder, "*.hbs", SearchOption.AllDirectories))
                {
                    var templateDir = PathUtils.GetRelativePath(folder, Path.GetDirectoryName(file)).Replace("\\", "/");
                    var templateName = Path.GetFileNameWithoutExtension(file);

                    if (!string.IsNullOrWhiteSpace(templateDir))
                    {
                        templateName = templateDir + "/" + templateName;
                    }

                    var template = new Template(templateName, Instance, TemplateSource.CreateForFile(file));

                    RegisterTemplate(template, true);
                }
            }
        }

        public void LoadTemplates(Assembly assembly, string templatePath, string templateNamePrefix)
        {
            var pattern = new Regex($@"^{Regex.Escape(templatePath)}\.(.+)\.hbs$", RegexOptions.IgnoreCase);

            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                var match = pattern.Match(resourceName);

                if (!match.Success)
                {
                    continue;
                }

                var contents = default(string);

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    contents = reader.ReadToEnd();
                }

                var templateName = templateNamePrefix + "/" + match.Groups[1].Value.Replace('.', '/');
                var template = new Template(templateName, Instance, TemplateSource.CreateForText(contents));

                RegisterTemplate(template, true);
            }
        }

        public void RegisterTemplate(ITemplate template, bool overrideExisting = false)
        {
            var internalTemplate = template as Template;

            if (internalTemplate == null)
            {
                throw new CodeGenerationException("Template must be of type HandlebarsNetTemplate");
            }

            if (_templateMap.ContainsKey(internalTemplate.Name))
            {
                if (!overrideExisting)
                {
                    throw new ConfigurationException($"Template '{internalTemplate.Name}' it already registered.");
                }
                else
                {
                    _templateMap.Remove(internalTemplate.Name);
                }
            }

            _templateMap.Add(internalTemplate.Name, internalTemplate);
            Instance.RegisterTemplate(internalTemplate.Name, (writer, data) =>
            {
                internalTemplate.GetRenderFn().Invoke(writer, data);
            });
        }
    }
}
