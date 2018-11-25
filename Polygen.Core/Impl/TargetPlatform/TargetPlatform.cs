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
        private readonly Dictionary<string, INamingConvention> _namingConventionMap = new Dictionary<string, INamingConvention>();
        private readonly Dictionary<string, IOutputModelGenerator> _outputModelGeneratorMap = new Dictionary<string, IOutputModelGenerator>();
        private readonly Dictionary<string, ITemplate> _outputTemplateMap = new Dictionary<string, ITemplate>();

        public TargetPlatform(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public INamingConvention GetNamingConvention(string language, bool throwIfMissing = true)
        {
            if (!_namingConventionMap.TryGetValue(language, out var res) && throwIfMissing)
            {
                throw new ConfigurationException($"Naming convention not configured for language '{language}' in target platform {Name}'.");
            }

            return res;
        }

        public void RegisterNamingConvention(string language, INamingConvention namingConvention, bool overwrite = true)
        {
            if (_namingConventionMap.ContainsKey(language))
            {
                if (overwrite)
                {
                    _namingConventionMap.Remove(language);
                }
                else
                {
                    throw new ConfigurationException($"Naming convention already configured for language '{language}' in target platform {Name}'.");
                }
            }

            _namingConventionMap[language] = namingConvention;
        }

        public IOutputModelGenerator GetOutputModelGenerator(string designModelType, bool throwIfMissing = true)
        {
            if (!_outputModelGeneratorMap.TryGetValue(designModelType, out var res) && throwIfMissing)
            {
                throw new ConfigurationException($"Output model generator not configured for design model type '{designModelType}' in target platform {Name}'.");
            }

            return res;
        }

        public void RegisterOutputModelGenerator(string designModelType, IOutputModelGenerator outputModelGenerator, bool overwrite)
        {
            if (_outputModelGeneratorMap.ContainsKey(designModelType))
            {
                if (overwrite)
                {
                    _outputModelGeneratorMap.Remove(designModelType);
                }
                else
                {
                    throw new ConfigurationException($"Output model generator already configured for design model type '{designModelType}' in target platform {Name}'.");
                }
            }

            _outputModelGeneratorMap[designModelType] = outputModelGenerator;

        }
        public ITemplate GetOutputTemplate(string outputModelType, bool throwIfMissing = true)
        {
            if (!_outputTemplateMap.TryGetValue(outputModelType, out var res) && throwIfMissing)
            {
                throw new ConfigurationException($"Output template not configured for output model type '{outputModelType}' in target platform {Name}'.");
            }

            return res;
        }

        public void RegisterOutputTemplate(string outputModelType, ITemplate outputTemplate, bool overwrite = true)
        {
            if (_outputTemplateMap.ContainsKey(outputModelType))
            {
                if (overwrite)
                {
                    _namingConventionMap.Remove(outputModelType);
                }
                else
                {
                    throw new ConfigurationException($"Output template already configured for output model type '{outputModelType}' in target platform {Name}'.");
                }
            }

            _outputTemplateMap[outputModelType] = outputTemplate;
        }
    }
}
