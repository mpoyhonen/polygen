using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polygen.Core.Exceptions;

namespace Polygen.Core.Utils
{
    /// <summary>
    /// Keeps track of depencencies so the entries can be processed in the correct order.
    /// </summary>
    public class DependencyMap<T>
    {
        private List<Registration> _registrations = new List<Registration>();
        private readonly ISet<string> _registrationIds = new HashSet<string>();
        private bool _sorted;

        public void Add(T item, string id, string[] dependsOn = null)
        {
            if (ContainsId(id))
            {
                throw new ArgumentException($"Item already added with with ID '{id}'.");
            }

            if (dependsOn != null && dependsOn.Contains(id))
            {
                throw new ArgumentException("Item cannot have a dependency on itself.");
            }

            _registrations.Add(new Registration(item, id, dependsOn, _registrations.Count + 1));
            _registrationIds.Add(id);
            _sorted = false;
        }

        public bool ContainsId(string id)
        {
            return _registrationIds.Contains(id);
        }

        public T Get(string id)
        {
            return _registrations
                .Where(x => x.Id == id)
                .Select(x => x.Item)
                .FirstOrDefault();
        }

        public IEnumerable<T> Entries
        {
            get
            {
                if (!_sorted)
                {
                    Sort();
                }

                return _registrations.Select(x => x.Item);
            }
        }

        private void Sort()
        {
            if (_registrations.Count <= 1)
            {
                _sorted = true;
                return;
            }

            var map = _registrations .ToDictionary(x => x.Id);
            var toAssign = new Dictionary<string, Registration>();
            var sortIndexCounter = 1;

            // Find all registrations each registration depends on.
            foreach (var registration in _registrations)
            {
                registration.Dependencies = new List<Registration>();

                foreach (var dependsOnId in registration.DependsOn ?? new string[0])
                {
                    if (map.TryGetValue(dependsOnId, out var reg))
                    {
                        registration.Dependencies.Add(reg);
                    }
                    else
                    {
                        throw new ConfigurationException($"Entry '{registration.Id}' has a dependency on missing entry '{dependsOnId}'.");
                    }
                }

                if (registration.Dependencies.Count == 0)
                {
                    registration.SortIndex = sortIndexCounter++;
                }
                else
                {
                    toAssign.Add(registration.Id, registration);
                }
            }

            // Assign the sort index to all registrations. If there is no change, then we have a loop.
            while (toAssign.Any())
            {
                var toRemove = new List<Registration>();
                var change = false;

                foreach (var registration in toAssign.Values.OrderBy(x => x.OriginalIndex).Where(x => x.Dependencies.All(y => y.SortIndex != null)))
                {
                    registration.SortIndex = sortIndexCounter++;
                    toRemove.Add(registration);
                    change = true;
                }

                if (!change)
                {
                    throw new ConfigurationException("Circular dependency with items: " + string.Join(", ", toAssign.Values.Select(x => x.Id)));
                }

                foreach (var registration in toRemove)
                {
                    toAssign.Remove(registration.Id);
                }
            }

            _registrations = _registrations.OrderBy(x => x.SortIndex).ToList();
            _sorted = true;
        }

        public class Registration
        {
            internal Registration(T item, string id, string[] dependsOn, int originalIndex)
            {
                Item = item;
                Id = id;
                DependsOn = dependsOn;
                OriginalIndex = originalIndex;
            }

            public T Item { get; }
            public string Id { get; }
            internal string[] DependsOn { get; }
            internal List<Registration> Dependencies { get; set; } 
            internal int OriginalIndex { get; set; }
            internal int? SortIndex { get; set; }
        }
    }
}
