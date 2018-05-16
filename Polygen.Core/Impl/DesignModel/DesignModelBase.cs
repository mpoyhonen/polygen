using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.OutputConfiguration;
using Polygen.Core.OutputModel;
using Polygen.Core.Parser;
using System.Collections.Generic;
using System.Linq;

namespace Polygen.Core.Impl.DesignModel
{
    /// <summary>
    /// Base class for parsed design models.
    /// </summary>
    public class DesignModelBase : IDesignModel
    {
        private readonly List<IOutputModel> _outputModels = new List<IOutputModel>();
        private readonly Dictionary<string, IDesignModelProperty> _propertyMap = new Dictionary<string, IDesignModelProperty>();
        private Dictionary<string, object> _customData;
        private string _fullyQualifiedName;

        public DesignModelBase(string name, string type, INamespace ns, IXmlElement element = null, IParseLocationInfo parseLocation = null)
        {
            Name = name;
            DesignModelType = type;
            Element = element;
            ParseLocation = parseLocation ?? element?.ParseLocation;
            Namespace = ns;
            
            if (ns != null)
            {
                OutputConfiguration = new OutputConfiguration.OutputConfiguration(ns.OutputConfiguration);
            }
        }

        public string DesignModelType { get; }
        public string Name { get; }
        public string FullyQualifiedName => GetFullyQualifiedName();
        public IXmlElement Element { get; }
        public IParseLocationInfo ParseLocation { get; }
        public INamespace Namespace { get; }
        public Dictionary<string, object> CustomData => _customData ?? (_customData = new Dictionary<string, object>());
        public IEnumerable<IDesignModelProperty> Properties => _propertyMap.Values;
        
        public void AddProperty(IDesignModelProperty property)
        {
            if (_propertyMap.ContainsKey(property.Name))
            {
                throw new DesignModelException(this, "Property '{property.Name}' already defined.");
            }
            
            _propertyMap.Add(property.Name, property);
        }

        public IDesignModelProperty GetProperty(string name, IParseLocationInfo parseLocation = null)
        {
            if (!_propertyMap.TryGetValue(name, out var property) && parseLocation != null)
            {
                throw new ParseException(parseLocation, $"Property '{name}' not found in design model '{FullyQualifiedName}'");
            }

            return property;
        }

        public virtual void CopyPropertiesFrom(IDesignModel source, IParseLocationInfo parseLocation = null)
        {
            var propertiesToCopy = source.Properties.Join(
                Element.Definition.Attributes,
                x => x.Name,
                x => x.Name.LocalName,
                (x, y) => new
                {
                    x.Name,
                    y.Type,
                    x.Value,
                    Definition = y
                }
            );

            foreach (var entry in propertiesToCopy.Where(x => !_propertyMap.ContainsKey(x.Name)))
            {
                AddProperty(new DesignModelProperty(entry.Name, entry.Type, entry.Value, entry.Definition, parseLocation));
            }
        }

        public IOutputConfiguration OutputConfiguration { get; }
        public IEnumerable<IOutputModel> OutputModels => _outputModels;

        public void AddOutputModel(IOutputModel outputModel)
        {
            if (_outputModels.Contains(outputModel))
            {
                throw new CodeGenerationException("Output model is already added to this design model.");
            }

            _outputModels.Add(outputModel);
        }
        
        private string GetFullyQualifiedName()
        {
            if (_fullyQualifiedName == null && Name != null && Namespace != null)
            {
                _fullyQualifiedName = Namespace.Name + "." + Name;
            }

            return _fullyQualifiedName;
        }
    }
}
