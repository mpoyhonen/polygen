using Polygen.Core.DesignModel;
using System.Collections.Generic;
using System.Linq;

namespace Polygen.Core.Impl.DesignModel
{
    public class DesignModelCollection : IDesignModelCollection
    {
        private readonly Dictionary<string, INamespace> _namespaceMap = new Dictionary<string, INamespace>();
        private readonly Dictionary<string, List<IDesignModel>> _designModelsByTypeMap = new Dictionary<string, List<IDesignModel>>();

        public DesignModelCollection()
        {
            this.RootNamespace = new Namespace(new OutputConfiguration.OutputConfiguration(null));
        }

        public INamespace RootNamespace { get; }

        public INamespace DefineNamespace(string ns)
        {
            if (!this._namespaceMap.TryGetValue(ns, out var res))
            {
                var parts = ns.Split('.');
                var parent = this.RootNamespace;

                foreach (var part in parts)
                {
                    var name = parent?.Name != null ? $"{parent.Name}.{part}" : part;

                    if (!this._namespaceMap.TryGetValue(name, out var segment))
                    {
                        segment = new Namespace(part, parent);
                        ((Namespace)parent).AddChild(segment);
                        this._namespaceMap.Add(segment.Name, segment);
                    }

                    parent = segment;
                }

                res = parent;
            }

            return res;
        }

        public IEnumerable<IDesignModel> GetByType(string type)
        {
            if (!this._designModelsByTypeMap.TryGetValue(type, out var list))
            {
                return Enumerable.Empty<IDesignModel>();
            }

            return list;
        }

        public INamespace GetNamespace(string name)
        {
            this._namespaceMap.TryGetValue(name, out var res);

            return res;
        }

        public IEnumerable<INamespace> GetAllNamespaces()
        {
            return this._namespaceMap.Values;
        }

        public void AddDesignModel(IDesignModel designModel)
        {
            if (!this._designModelsByTypeMap.TryGetValue(designModel.Type, out var list))
            {
                list = new List<IDesignModel>();
                this._designModelsByTypeMap[designModel.Type] = list;
            }

            list.Add(designModel);
        }

        public IEnumerable<IDesignModel> GetAllDesignModels()
        {
            return this._designModelsByTypeMap
                .Values
                .SelectMany(x => x);
        }
    }
}
