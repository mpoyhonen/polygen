using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polygen.Core.Exceptions;
using Polygen.Core.Stage;
using Polygen.Core.Utils;

namespace Polygen.Core.Impl.Stage
{
    public class StageHandlerRegistry: IStageHandlerRegistry
    {
        private readonly Dictionary<StageType, DependencyMap<IStageHandler>> _hanlerMap = new Dictionary<StageType, DependencyMap<IStageHandler>>();

        public StageHandlerRegistry(
            IEnumerable<IStageHandler> stageHandlers
        )
        {
            foreach (var handler in stageHandlers)
            {
                if (!this._hanlerMap.ContainsKey(handler.Stage))
                {
                    this._hanlerMap[handler.Stage] = new DependencyMap<IStageHandler>();
                }

                var handlerMap = this._hanlerMap[handler.Stage];

                if (handlerMap.ContainsId(handler.Id))
                {
                    var previousHandlerType = handlerMap.Get(handler.Id).GetType();
                    var currentHandlerType = handler.GetType();

                    throw new ConfigurationException($"Stage handler with ID '{handler.Id}' already registered for stage '{handler.Stage}'. Existing handler type '{previousHandlerType.FullName}', trying to register type '{currentHandlerType}'.");
                }

                handlerMap.Add(handler, handler.Id, handler.Dependencies);
            }
        }

        public IEnumerable<IStageHandler> GetHandlers(StageType stage)
        {
            if (this._hanlerMap.TryGetValue(stage, out var map))
            {
                return map.Entries;
            }
            else
            {
                return Enumerable.Empty<IStageHandler>();
            }
        }
    }
}
