using Polygen.Core.DesignModel;
using Polygen.Core.OutputConfiguration;
using System.Collections.Generic;
using System.Linq;
using Polygen.Core.Exceptions;
using Polygen.Core.Utils;

namespace Polygen.Core.Impl.DesignModel
{
    public class Namespace : INamespace
    {
        private readonly List<INamespace> _children = new List<INamespace>();
        private readonly List<IDesignModel> _designModelList = new List<IDesignModel>();
        private readonly MultiKeyListDictionary<IDesignModel> _designModelMap = new MultiKeyListDictionary<IDesignModel>();

        public Namespace(IOutputConfiguration mainOutputConfiguration)
        {
            OutputConfiguration = mainOutputConfiguration;
        }

        public Namespace(string name, INamespace parent)
        {
            Name = parent?.Name != null ? $"{parent.Name}.{name}" : name;
            SegmentName = name;
            OutputConfiguration = new OutputConfiguration.OutputConfiguration(parent?.OutputConfiguration);
        }

        public string Name { get; }
        public string SegmentName { get; }
        public INamespace Parent { get; set; }
        public IReadOnlyList<INamespace> Children => _children;
        public IEnumerable<IDesignModel> DesignModels => _designModelList;
        public IOutputConfiguration OutputConfiguration { get; }

        internal void AddChild(INamespace ns)
        {
            _children.Add(ns);
        }

        /// <inheritdoc />
        public void AddDesignModel(IDesignModel designModel)
        {
            _designModelList.Add(designModel);

            _designModelMap.Add(nameof(IDesignModel.DesignModelType), designModel.DesignModelType, designModel);
            _designModelMap.Add(nameof(IDesignModel.Name), designModel.Name, designModel);
        }

        /// <inheritdoc />
        public IEnumerable<IDesignModel> FindDesignModelsByType(string type, bool recursive = true)
        {
            foreach (var designModel in _designModelMap.Get(nameof(IDesignModel.DesignModelType), type))
            {
                yield return designModel;
            }

            if (recursive)
            {
                foreach (var designModel in _children.Select(x => x.FindDesignModelsByType(type, recursive)).SelectMany(x => x))
                {
                    yield return designModel;
                }
            }
        }

        public T GetDesignModel<T>(DesignModelReference<T> reference, bool throwIfNotFound) where T: IDesignModel
        {
            if (reference.Target != null)
            {
                return reference.Target;
            }

            if (reference.Type != null)
            {
                foreach (var designModel in _designModelMap.Get(nameof(IDesignModel.Name), reference.Name))
                {
                    if (designModel.DesignModelType == reference.Type)
                    {
                        return (T) designModel;
                    }
                }

                if (throwIfNotFound)
                {
                    throw new ParseException(reference.ParseLocation, $"{reference.Type} '{Name}.{reference.Name}' not found");
                }
            }
            else
            {
                var designModels = _designModelMap.Get(nameof(IDesignModel.Name), reference.Name).ToList();

                if (designModels.Count == 1)
                {
                    return (T) designModels.First();
                }

                var error = designModels.Count > 1 
                    ? $"More than one design model with name '{reference.Name}' found in namespace '{Name}'. Specify the design model type by using 'type:Name'" 
                    : $"No design model with name '{reference.Name}' found in namespace '{Name}'.";

                if (throwIfNotFound)
                {
                    throw new ParseException(reference.ParseLocation, error);
                }
            }
            
            return default(T);
        }

        private bool Equals(INamespace other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Namespace) obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
