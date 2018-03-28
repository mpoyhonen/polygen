using System;
using System.Collections.Generic;
using System.Text;

namespace Polygen.Core.Stage
{
    /// <summary>
    /// Base class for stage handlers.
    /// </summary>
    public abstract class StageHandlerBase : IStageHandler
    {
        public StageHandlerBase(StageType stageType, string Id, params string[] dependencies)
        {
            this.Stage = stageType;
            this.Id = Id;
            this.Dependencies = dependencies;
        }

        public string Id { get; protected set; }
        public string[] Dependencies { get; protected set; }
        public StageType Stage { get; protected set; }
        public abstract void Execute();
    }
}
