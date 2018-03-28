using System.Collections.Generic;

namespace Polygen.Core.Project
{
    /// <summary>
    /// Interface accessing information about one project in the solution.
    /// </summary>
    public interface IProject
    {
        /// <summary>
        /// Project name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Project source folder where current contents are read from. This is also the destination folder for code generation.
        /// </summary>
        string SourceFolder { get; }
        /// <summary>
        /// Project temp folder. Code is generated here before it is copied to the source folder.
        /// </summary>
        string TempFolder { get; }
        /// <summary>
        /// Defines the project type (web, design, etc.)
        /// </summary>
        string Type { get; }
        /// <summary>
        /// Project root folder
        /// </summary>
        IProjectFolder ProjectFolder { get; }
        /// <summary>
        /// Path to the project file.
        /// </summary>
        IProjectFile ProjectFile { get; }
        /// <summary>
        /// Whether this project is part of a Visual Studio solution.
        /// </summary>
        bool IsVisualStudioProject { get; }
        /// <summary>
        /// Returns all design model files under this project.
        /// </summary>
        IEnumerable<IProjectFile> DesignModelFiles { get; }

        /// <summary>
        /// Returns a file under this project.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IProjectFile GetFile(string path);
        /// <summary>
        /// Returns a sunbfolder under this project.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IProjectFolder GetFolder(string path);
    }
}
