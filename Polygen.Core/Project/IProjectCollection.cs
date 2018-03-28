using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygen.Core.Project;

namespace Polygen.Core.Project
{
    /// <summary>
    /// Interface for accessing configured projects.
    /// </summary>
    public interface IProjectCollection
    {
        /// <summary>
        /// Returns all configured projects.
        /// </summary>
        IReadOnlyList<IProject> Projects { get; }
        /// <summary>
        /// Returns a project by name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="throwIfMissing"></param>
        /// <returns></returns>
        IProject GetProjectByName(string name, bool throwIfMissing = true);
        /// <summary>
        /// Returns all projects matching the type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<IProject> GetProjectsByType(string type);
        /// <summary>
        /// Returns the first project matching the type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="throwIfMissing"></param>
        /// <returns></returns>
        IProject GetFirstProjectByType(string type, bool throwIfMissing = true);
        /// <summary>
        /// Adds a project to this collection.
        /// </summary>
        /// <param name="project"></param>
        void AddProject(IProject project);
    }
}
