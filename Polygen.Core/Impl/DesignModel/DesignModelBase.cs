using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.OutputConfiguration;
using Polygen.Core.OutputModel;
using Polygen.Core.Parser;
using System.Collections.Generic;

namespace Polygen.Core.Impl.DesignModel
{
    /// <summary>
    /// Base class for parsed design models.
    /// </summary>
    public class DesignModelBase : IDesignModel
    {
        private readonly List<IOutputModel> _outputModels = new List<IOutputModel>();
        private Dictionary<string, object> _customData;
        private string _fullyQualifiedName;

        public DesignModelBase(string type, INamespace ns, IXmlElement element)
        {
            this.Type = type;
            this.Element = element;
            this.Namespace = ns;
            this.OutputConfiguration = new OutputConfiguration.OutputConfiguration(ns.OutputConfiguration);
        }

        public string Type { get; protected set; }
        public string Name { get; set; }
        public string FullyQualifiedName => this.GetFullyQualifiedName();
        public IXmlElement Element { get; protected set; }
        public INamespace Namespace { get; protected set; }
        public Dictionary<string, object> CustomData
        {
            get { return this._customData ?? (this._customData = new Dictionary<string, object>()); }
        }
        public IOutputConfiguration OutputConfiguration { get; }
        public IEnumerable<IOutputModel> OutputModels => this._outputModels;

        public void AddOutputModel(IOutputModel outputModel)
        {
            if (this._outputModels.Contains(outputModel))
            {
                throw new CodeGenerationException("Output model is already added to this design model.");
            }

            this._outputModels.Add(outputModel);
        }

        private string GetFullyQualifiedName()
        {
            if (this._fullyQualifiedName == null && this.Name != null)
            {
                this._fullyQualifiedName = this.Namespace.Name + "." + this.Name;
            }

            return this._fullyQualifiedName;
        }
    }
}
