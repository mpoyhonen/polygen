using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygen.Core.Project;

namespace Polygen.Core.Project
{
    /// <summary>
    /// Interface for accessing the project configuration data read from ProjectConfiguration.xml file.
    /// </summary>
    public interface IProjectConfiguration
    {
        /// <summary>
        /// Project name.
        /// </summary>
        string ProjectName { get; }
        /// <summary>
        /// Contains all configured projects.
        /// </summary>
        IProjectCollection Projects { get; }
    }
}
