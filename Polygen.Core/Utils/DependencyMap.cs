using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polygen.Core.Utils
{
    /// <summary>
    /// Keeps track of depencencies so the entries can be processed in the correct order.
    /// </summary>
    public class DependencyMap<T>
    {
        private List<Registration> _registrations = new List<Registration>();
        private bool _sorted;

        public void Add(T item, string id, string[] dependsOn = null)
        {
            if (this.ContainsId(id))
            {
                throw new ArgumentException($"Item already added with with ID '{id}'.");
            }

            this._registrations.Add(new Registration(item, id, dependsOn));
            this._sorted = false;
        }

        public bool ContainsId(string id)
        {
            return this._registrations.Any(x => x.Id == id);
        }

        public T Get(string id)
        {
            return this._registrations
                .Where(x => x.Id == id)
                .Select(x => x.Item)
                .FirstOrDefault();
        }

        public IEnumerable<T> Entries
        {
            get
            {
                if (!this._sorted)
                {
                    this.Sort();
                }

                return this._registrations.Select(x => x.Item);
            }
        }

        private void Sort()
        {
            if (this._registrations.Count <= 1)
            {
                return;
            }

            var map = this._registrations
                .ToDictionary(x => x.Id);
            var toAssign = new List<Registration>(this._registrations);
            var sortIndexCounter = 1;

            // Find all registrations each registration depends on.
            foreach (var registration in this._registrations)
            {
                registration.Dependencies = new List<Registration>();

                foreach (var dependsOnId in registration.DependsOn ?? new string[0])
                {
                    if (map.TryGetValue(dependsOnId, out var reg))
                    {
                        registration.Dependencies.Add(reg);
                    }
                }

                if (registration.Dependencies.Count == 0)
                {
                    registration.SortIndex = sortIndexCounter++;
                }
                else
                {
                    toAssign.Add(registration);
                }
            }

            // Assign the sort index to all registrations. If there is no change, then we have a loop.
            while (toAssign.Count() > 0)
            {
                var toRemove = new List<Registration>();
                var change = false;

                toAssign
                    .Where(x => x.Dependencies.All(y => y.SortIndex != null))
                    .Select(x =>
                    {
                        x.SortIndex = sortIndexCounter++;
                        return x;
                    })
                    .ToList()
                    .Select(x =>
                    {
                        toAssign.Remove(x);
                        change = true;
                        return x;
                    })
                    .ToList();

                if (!change)
                {
                    throw new ArgumentException("Circular dependency with items: " + string.Join(", ", toAssign.Select(x => x.Id)));
                }
            }

            this._sorted = true;
        }

        public class Registration
        {
            internal Registration(T item, string id, string[] dependsOn)
            {
                this.Item = item;
                this.Id = id;
                this.DependsOn = dependsOn;
            }

            public T Item { get; }
            public string Id { get; }
            internal string[] DependsOn { get; }
            internal List<Registration> Dependencies { get; set; } 
            internal int? SortIndex { get; set; }
        }
    }
}
