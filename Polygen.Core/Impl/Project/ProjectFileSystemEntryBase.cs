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
            RelativePath = path;
            _isFolder = isFolder;
            _name = Path.GetFileName(path);
        }

        public ProjectFileSystemEntryBase(IProject project, string relativePath, bool isFolder) : this(relativePath, isFolder)
        {
            Project = project;
        }

        public IProject Project { get; }
        public string RelativePath { get; }
        public string Name => _name ?? (_name = Path.GetFileName(RelativePath));
        public string Extension => _extension ?? (_extension = Path.GetExtension(RelativePath));

        public string GetSourcePath(bool throwIfNotSet = false)
        {
            if (_sourcePath == null)
            {
                if (Project == null)
                {
                    if (throwIfNotSet)
                    {
                        throw new ConfigurationException($"Project is not set for file '{RelativePath}'.");
                    }
                }
                else
                {
                    if (Project.SourceFolder == null)
                    {
                        throw new ConfigurationException($"Project '{Project.Name}' source folder is not set.");
                    }

                    _sourcePath = Path.Combine(Project.SourceFolder, RelativePath);
                }

                if (_sourcePath == null && throwIfNotSet)
                {
                    throw new ConfigurationException($"Source path is not set for file '{RelativePath}'.");
                }
            }

            return _sourcePath;
        }

        public string GetTempPath(bool throwIfNotSet = false)
        {
            if (_tempPath == null)
            {
                if (Project == null)
                {
                    if (throwIfNotSet)
                    {
                        throw new ConfigurationException($"Project is not set for file '{RelativePath}'.");
                    }
                }
                else
                {
                    if (Project.TempFolder == null)
                    {
                        throw new ConfigurationException($"Project '{Project.Name}' temp folder is not set.");
                    }

                    _tempPath = Path.Combine(Project.TempFolder, RelativePath);
                }

                if (_tempPath == null && throwIfNotSet)
                {
                    throw new ConfigurationException($"Temp path is not set for file '{RelativePath}'.");
                }
            }

            return _tempPath;
        }

        #region Equality comparison

        private bool Equals(ProjectFileSystemEntryBase other)
        {
            return Project.Equals(other.Project) && string.Equals(RelativePath, other.RelativePath);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((ProjectFileSystemEntryBase) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Project.GetHashCode() * 397) ^ RelativePath.GetHashCode();
            }
        }       
        
        #endregion
    }
}
