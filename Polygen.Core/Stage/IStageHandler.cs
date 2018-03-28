using System;
using System.Collections.Generic;
using System.Text;

namespace Polygen.Core.Stage
{
    /// <summary>
    /// Interface for a handler which plugs into the code generation process.
    /// </summary>
    public interface IStageHandler
    {
        /// <summary>
        /// Stage handler ID. Used to for other handler to declare dependencies to this handler.
        /// </summary>
        string Id { get; }
        /// <summary>
        /// Declares that this handler must be executed after the specified handlers.
        /// </summary>
        string[] Dependencies { get; }
        /// <summary>
        /// Type of this stage this handler is attached to.
        /// </summary>
        StageType Stage { get; }
        /// <summary>
        /// Called when the specified stage has been reached.
        /// </summary>
        void Execute();
    }
}
