using System;
using System.Collections.Generic;
using System.Text;
using Polygen.Core.Exceptions;
using Polygen.Core.TargetPlatform;

namespace Polygen.Core.Impl.TargetPlatform
{
    public class TargetPlatformCollection : ITargetPlatformCollection
    {
        private Dictionary<string, ITargetPlatform> _map = new Dictionary<string, ITargetPlatform>();

        public ITargetPlatform GetTargetPlatform(string name, bool throwIfMissing = true)
        {
            if (!_map.TryGetValue(name, out var targetPlatform) && throwIfMissing)
            {
                throw new ConfigurationException($"Target platform '{name}' not found.");
            }

            return targetPlatform;
        }

        public void RegisterTargetPlatform(ITargetPlatform targetPlatform, bool overwrite = false)
        {
            if (_map.ContainsKey(targetPlatform.Name))
            {
                if (overwrite)
                {
                    _map.Remove(targetPlatform.Name);
                }
                else
                {
                    throw new ConfigurationException($"Target platform '{targetPlatform.Name}' is already registered.");
                }
            }

            _map[targetPlatform.Name] = targetPlatform;
        }
    }
}
