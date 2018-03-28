using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polygen.Core.Utils
{
    /// <summary>
    /// Used to register plugin event listeners and fire events. Listeners can
    /// declare dependecies to other plugins, so that the events are handled in a correct order.
    /// </summary>
    public class EventHub<EventT>
    {
        private DependencyMap<Action<EventT>> _registrations = new DependencyMap<Action<EventT>>();

        public void AddListener(Action<EventT> handler, string pluginId, string[] dependsOnPlugins = null)
        {
            this._registrations.Add(handler, pluginId, dependsOnPlugins);
        }

        public void FireEvent(EventT evt)
        {
            foreach (var handler in this._registrations.Entries)
            {
                handler(evt);
            }
        }
    }
}
