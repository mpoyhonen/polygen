using Polygen.Core.DesignModel;
using Polygen.Core.Exceptions;
using Polygen.Core.NamingConvention;
using Polygen.Core.OutputConfiguration;
using Polygen.Core.Project;
using Polygen.Core.TargetPlatform;
using Polygen.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Polygen.Core.Impl.OutputConfiguration
{
    public class OutputConfiguration : IOutputConfiguration
    {
        private readonly ListDictionary<string, ITargetPlatform> _targetPlatformByDesignModelTypeMap = new ListDictionary<string, ITargetPlatform>();
        private readonly List<Tuple<Filter, IProjectFolder>> _outputFolderList = new List<Tuple<Filter, IProjectFolder>>();

        public OutputConfiguration(IOutputConfiguration parent)
        {
            this.Parent = parent;
        }

        public IOutputConfiguration Parent { get; }

        public IEnumerable<ITargetPlatform> GetTargetPlatformsForDesignModel(IDesignModel designModel)
        {
            var res = _targetPlatformByDesignModelTypeMap.GetOrNull(designModel.DesignModelType);
            
            if (res == null && Parent != null)
            {
                return Parent.GetTargetPlatformsForDesignModel(designModel);
            }

            return res ?? Enumerable.Empty<ITargetPlatform>();
        }

        public void RegisterTargetPlatformForDesignModelType(string designModelType, ITargetPlatform targetPlatform, bool replace = false)
        {
            if (replace && _targetPlatformByDesignModelTypeMap.ContainsKey(designModelType))
            {
                _targetPlatformByDesignModelTypeMap.Remove(designModelType);
            }

            _targetPlatformByDesignModelTypeMap.Add(designModelType, targetPlatform);
        }

        public IProjectFolder GetOutputFolder(string outputModelType, bool throwIfMissing = true)
        {
            foreach (var entry in this._outputFolderList)
            {
                if (entry.Item1.Match(outputModelType) == Filter.MatchStatus.Included)
                {
                    return entry.Item2;
                }
            }

            if (this.Parent == null)
            {
                throw new ConfigurationException($"No output folder configured project output model type '{outputModelType}'");
            }

            return this.Parent.GetOutputFolder(outputModelType, throwIfMissing);
        }

        public void RegisterOutputFolder(Filter outputModelTypeFilter, IProjectFolder projectFolder, bool overwrite = true)
        {
            var prevItem = this._outputFolderList.FirstOrDefault(x => x.Item1.FilterEquals(outputModelTypeFilter));

            if (prevItem != null)
            {
                if (overwrite)
                {
                    this._outputFolderList.Remove(prevItem);
                }
                else
                {
                    throw new ConfigurationException($"Output folder already configured for output model type '{outputModelTypeFilter.ToString()}'");
                }
            }

            this._outputFolderList.Add(Tuple.Create(outputModelTypeFilter, projectFolder));
        }
    }
}
