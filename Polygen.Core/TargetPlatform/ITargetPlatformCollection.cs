using System;
using System.Collections.Generic;
using System.Text;

namespace Polygen.Core.TargetPlatform
{
    /// <summary>
    /// Interface for accessing configured target platforms.
    /// </summary>
    public interface ITargetPlatformCollection
    {
        /// <summary>
        /// Returns target platform by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ITargetPlatform GetTargetPlatform(string name, bool throwIfMissing = true);
        /// <summary>
        /// Registers a new target platform into the collection.
        /// </summary>
        /// <param name="targetPlatform"></param>
        /// <param name="overwrite"></param>
        void RegisterTargetPlatform(ITargetPlatform targetPlatform, bool overwrite = false);
    }
}
