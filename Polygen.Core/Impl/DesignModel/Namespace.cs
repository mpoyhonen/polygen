using Polygen.Core.DesignModel;
using Polygen.Core.OutputConfiguration;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Polygen.Core.Exceptions;
using Polygen.Core.Parser;

namespace Polygen.Core.Impl.DesignModel
{
    [DebuggerDisplay("Namespace: {Name}")]
    public class Namespace : INamespace
    {
        private readonly List<INamespace> _children = new List<INamespace>();
        private readonly List<IDesignModel> _designModelList = new List<IDesignModel>();
        private readonly Dictionary<string, List<IDesignModel>> _designModelsByTypeMap = new Dictionary<string, List<IDesignModel>>();

        public Namespace(IOutputConfiguration mainOutputConfiguration)
        {
            this.OutputConfiguration = mainOutputConfiguration;
        }

        public Namespace(string name, INamespace parent)
        {
            this.Name = parent?.Name != null ? $"{parent.Name}.{name}" : name;
            this.SegmentName = name;
            this.OutputConfiguration = new OutputConfiguration.OutputConfiguration(parent?.OutputConfiguration);
        }

        public string Name { get; }
        public string SegmentName { get; }
        public INamespace Parent { get; set; }
        public IReadOnlyList<INamespace> Children => this._children;
        public IEnumerable<IDesignModel> DesignModels => this._designModelList;
        public IOutputConfiguration OutputConfiguration { get; }

        internal void AddChild(INamespace ns)
        {
            this._children.Add(ns);
        }

        public void AddDesignModel(IDesignModel designModel)
        {
            this._designModelList.Add(designModel);

            if (!this._designModelsByTypeMap.TryGetValue(designModel.Type, out var typeList))
            {
                typeList = new List<IDesignModel>();
                this._designModelsByTypeMap[designModel.Type] = typeList;
            }

            typeList.Add(designModel);
        }

        public IEnumerable<IDesignModel> FindDesignModelsByType(string type, bool recursive = true)
        {
            if (this._designModelsByTypeMap.TryGetValue(type, out var typeList))
            {
                foreach (var designModel in typeList)
                {
                    yield return designModel;
                }
            }

            if (recursive)
            {
                foreach (var designModel in this._children.Select(x => x.FindDesignModelsByType(type, recursive)).SelectMany(x => x))
                {
                    yield return designModel;
                }
            }
        }

        public IDesignModel GetDesignModel(string type, string name, IParseLocationInfo parseLocation = null)
        {
            foreach (var designModel in FindDesignModelsByType(type, false))
            {
                if (designModel.Name == name)
                {
                    return designModel;
                }
            }

            if (parseLocation != null)
            {
                throw new ParseException(parseLocation, $"{type} '{Name}.{name}' not found");
            }
            else
            {
                return null;
            }
        }
    }
}
