using Polygen.Core.Exceptions;
using Polygen.Core.Project;
using System.IO;

namespace Polygen.Core.Impl.Project
{
    /// <summary>
    /// Base class for ProjectFile and ProjectFolder.
    /// </summary>
    public class ProjectFileSystemEntryBase
    {
        protected bool _isFolder;
        protected string _sourcePath;
        protected string _tempPath;
        protected string _name;
        protected string _extension;

        public ProjectFileSystemEntryBase(string path, bool isFolder)
        {
            this.RelativePath = path;
            this._isFolder = isFolder;
            this._name = Path.GetFileName(path);
        }

        public ProjectFileSystemEntryBase(IProject project, string relativePath, bool isFolder) : this(relativePath, isFolder)
        {
            this.Project = project;
        }

        public IProject Project { get; }
        public string RelativePath { get; }
        public string Name => this._name ?? (this._name = Path.GetFileName(this.RelativePath));
        public string Extension => this._extension ?? (this._extension = Path.GetExtension(this.RelativePath));

        public string GetSourcePath(bool throwIfNotSet = false)
        {
            if (this._sourcePath == null)
            {
                if (this.Project == null)
                {
                    if (throwIfNotSet)
                    {
                        throw new ConfigurationException($"Project is not set for file '{this.RelativePath}'.");
                    }
                }
                else
                {
                    if (this.Project.SourceFolder == null)
                    {
                        throw new ConfigurationException($"Project '{this.Project.Name}' source folder is not set.");
                    }

                    this._sourcePath = Path.Combine(this.Project.SourceFolder, this.RelativePath);
                }

                if (this._sourcePath == null && throwIfNotSet)
                {
                    throw new ConfigurationException($"Source path is not set for file '{this.RelativePath}'.");
                }
            }

            return this._sourcePath;
        }

        public string GetTempPath(bool throwIfNotSet = false)
        {
            if (this._tempPath == null)
            {
                if (this.Project == null)
                {
                    if (throwIfNotSet)
                    {
                        throw new ConfigurationException($"Project is not set for file '{this.RelativePath}'.");
                    }
                }
                else
                {
                    if (this.Project.TempFolder == null)
                    {
                        throw new ConfigurationException($"Project '{this.Project.Name}' temp folder is not set.");
                    }

                    this._tempPath = Path.Combine(this.Project.TempFolder, this.RelativePath);
                }

                if (this._tempPath == null && throwIfNotSet)
                {
                    throw new ConfigurationException($"Temp path is not set for file '{this.RelativePath}'.");
                }
            }

            return this._tempPath;
        }
    }
}
