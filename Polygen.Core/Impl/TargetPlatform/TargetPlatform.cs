using System;
using System.Collections.Generic;
using System.Text;
using Polygen.Core.Exceptions;
using Polygen.Core.NamingConvention;
using Polygen.Core.OutputModel;
using Polygen.Core.TargetPlatform;
using Polygen.Core.Template;

namespace Polygen.Core.Impl.TargetPlatform
{
    public class TargetPlatform : ITargetPlatform
    {
        private Dictionary<string, IClassNamingConvention> _classNamingConventionMap = new Dictionary<string, IClassNamingConvention>();
        private Dictionary<string, IOutputModelGenerator> _outputModelGeneratorMap = new Dictionary<string, IOutputModelGenerator>();
        private Dictionary<string, ITemplate> _outputTemplateMap = new Dictionary<string, ITemplate>();

        public TargetPlatform(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public IClassNamingConvention GetClassNamingConvention(string language, bool throwIfMissing = true)
        {
            if (!this._classNamingConventionMap.TryGetValue(language, out var res) && throwIfMissing)
            {
                throw new ConfigurationException($"Class naming convention not configured for language '{language}' in target platform {this.Name}'.");
            }

            return res;
        }

        public void RegisterClassNamingConvention(string language, IClassNamingConvention namingConvention, bool overwrite = true)
        {
            if (this._classNamingConventionMap.ContainsKey(language))
            {
                if (overwrite)
                {
                    this._classNamingConventionMap.Remove(language);
                }
                else
                {
                    throw new ConfigurationException($"Class naming convention already configured for language '{language}' in target platform {this.Name}'.");
                }
            }

            this._classNamingConventionMap[language] = namingConvention;
        }

        public IOutputModelGenerator GetOutputModelGenerator(string designModelType, bool throwIfMissing = true)
        {
            if (!this._outputModelGeneratorMap.TryGetValue(designModelType, out var res) && throwIfMissing)
            {
                throw new ConfigurationException($"Output model generator not configured for design model type '{designModelType}' in target platform {this.Name}'.");
            }

            return res;
        }

        public void RegisterOutputModelGenerator(string designModelType, IOutputModelGenerator outputModelGenerator, bool overwrite)
        {
            if (this._outputModelGeneratorMap.ContainsKey(designModelType))
            {
                if (overwrite)
                {
                    this._outputModelGeneratorMap.Remove(designModelType);
                }
                else
                {
                    throw new ConfigurationException($"Output model generator already configured for design model type '{designModelType}' in target platform {this.Name}'.");
                }
            }

            this._outputModelGeneratorMap[designModelType] = outputModelGenerator;

        }
        public ITemplate GetOutputTemplate(string outputModelType, bool throwIfMissing = true)
        {
            if (!this._outputTemplateMap.TryGetValue(outputModelType, out var res) && throwIfMissing)
            {
                throw new ConfigurationException($"Output template not configured for output model type '{outputModelType}' in target platform {this.Name}'.");
            }

            return res;
        }

        public void RegisterOutputTemplate(string outputModelType, ITemplate outputTemplate, bool overwrite = true)
        {
            if (this._outputTemplateMap.ContainsKey(outputModelType))
            {
                if (overwrite)
                {
                    this._classNamingConventionMap.Remove(outputModelType);
                }
                else
                {
                    throw new ConfigurationException($"Output tempalte already configured for output model type '{outputModelType}' in target platform {this.Name}'.");
                }
            }

            this._outputTemplateMap[outputModelType] = outputTemplate;
        }
    }
}
