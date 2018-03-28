using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Polygen.Core.NamingConvention;
using Polygen.Core.Utils;

namespace Polygen.Core.Project
{
    /// <summary>
    /// Interface for a project layout which defines how output models are configured
    /// to be written to the file system. It is also possible to configure naming
    /// conventions for the output models.
    ///
    /// This structure is converted to OutputConfiguration objects once the design models
    /// have been parsed.
    /// </summary>
    public interface IProjectLayout
    {
        /// <summary>
        /// Contains the configured entries.
        /// </summary>
        ReadOnlyCollection<ProjectLayoutEntry> Entries { get; }

        /// <summary>
        /// Adds a new entry.
        /// </summary>
        /// <param name="entry"></param>
        void AddEntry(ProjectLayoutEntry entry);
    }

    /// <summary>
    /// Contains the data for one project layout entry. These entries are applied 
    /// to any design model or output model which match the configured filters.
    /// </summary>
    public class ProjectLayoutEntry
    {
        /// <summary>
        /// Filters the subset of namespaces and design models this entry applies to.
        /// </summary>
        public ProjectLayoutLocationFilter[] LocationFilters { get; set; }
        /// <summary>
        /// Defines target platforms for design models.
        /// </summary>
        public ProjectLayoutTargetPlatform[] TargetPlatforms { get; set; }
        /// <summary>
        /// Defines the output folders of output models.
        /// </summary>
        public ProjectLayoutOutput[] Outputs { get; set; }
    }

    public class ProjectLayoutLocationFilter
    {
        /// <summary>
        /// Design model namespace filter.
        /// </summary>
        public Filter NamespaceFilter { get; set; }
        /// <summary>
        /// Design model type filters.
        /// </summary>
        public Filter DesignModelTypeFilter { get; set; }
        /// <summary>
        /// Design model name filters.
        /// </summary>
        public Filter DesignModelNameFilter { get; set; }
    }

    public class ProjectLayoutTargetPlatform
    {
        /// <summary>
        /// Type of the design model to match.
        /// </summary>
        public string DesignModelType { get; set; }
        /// <summary>
        /// Target platform to use for this design model.
        /// </summary>
        public string TargerPlatformName { get; set; }
    }

    public class ProjectLayoutOutput
    {
        /// <summary>
        /// Defines which output models match this entry.
        /// </summary>
        public Filter OutputModelTypeFilter { get; set; } 
        /// <summary>
        /// Type of the project this entry applies to. Only applicaple when FolderPath is set and project name is not set.
        /// </summary>
        public string ProjectType { get; set; }
        /// <summary>
        /// Name of the project this entry applies to. Only applicaple when FolderPath is set and project type is not set.
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// Project folder where the output files are written to.
        /// </summary>
        public string FolderPath { get; set; }
    }
}
