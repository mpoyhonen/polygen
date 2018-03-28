using System;
using System.Collections.Generic;
using System.Text;
using Polygen.Core.Utils;

namespace Polygen.Core.Stage
{
    /// <summary>
    /// Contains all specific stage handlers which are going to be used during
    /// code generation.
    /// </summary>
    public interface IStageHandlerRegistry
    {
        /// <summary>
        /// Returns all registered stage handlers for the given stage.
        /// </summary>
        IEnumerable<IStageHandler> GetHandlers(StageType stage);
    }
}
