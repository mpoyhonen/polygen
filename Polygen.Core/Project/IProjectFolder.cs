namespace Polygen.Core.Project
{
    /// <summary>
    /// Represents a filesystem folder under a project.
    /// </summary>
    public interface IProjectFolder
    {
        /// <summary>
        /// Owning project.
        /// </summary>
        IProject Project { get; }
        /// <summary>
        /// Folder path relative to the project.
        /// </summary>
        string RelativePath { get; }
        /// <summary>
        /// Folder name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns absolute folder path in the project source folder.
        /// </summary>
        /// <param name="throwIfNotSet"></param>
        string GetSourcePath(bool throwIfNotSet = false);
        /// <summary>
        /// Returns absolute folder path in the project temp folder.
        /// </summary>
        /// <param name="throwIfNotSet"></param>
        string GetTempPath(bool throwIfNotSet = false);
        /// <summary>
        /// Returns a file under this folder.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IProjectFile GetFile(string path);
        /// <summary>
        /// Returns a sunbfolder under this folder.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IProjectFolder GetFolder(string path);
    }
}