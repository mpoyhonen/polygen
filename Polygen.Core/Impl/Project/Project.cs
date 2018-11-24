using Polygen.Core.Exceptions;
using Polygen.Core.Project;
using Polygen.Core.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace Polygen.Core.Impl.Project
{
    [DebuggerDisplay("Project: {Name}, Source: {SourceFolder}, Temp: {TempFolder}")]
    public class Project : IProject
    {
        private string _designModelFolders;

        public Project(string name, string type, string sourceFolder)
        {
            Name = name;
            Type = type;
            SourceFolder = sourceFolder;
            ProjectFolder = new ProjectFolder(this, "");
            ProjectFile = new ProjectFile(this, Name + ".csproj");
        }

        public string Name { get; }
        public string SourceFolder { get; }
        public string TempFolder { get; private set; }
        public string Type { get; }
        public IProjectFile ProjectFile { get; set; }
        public IProjectFolder ProjectFolder { get; }
        public bool IsVisualStudioProject { get; set; }
        public IEnumerable<IProjectFile> DesignModelFiles => GetDesignModelFiles();

        public void SetTempFolder(string tempFolder)
        {
            TempFolder = tempFolder;
        }

        public void SetDesignModelsFolder(string folders)
        {
            _designModelFolders = folders;
        }

        private IEnumerable<IProjectFile> GetDesignModelFiles()
        {
            if (string.IsNullOrWhiteSpace(_designModelFolders))
            {
                yield break;
            }

            if (string.IsNullOrWhiteSpace(SourceFolder))
            {
                throw new ConfigurationException("Source folder must be set.");
            }

            var scanner = new FolderScanner(Path.GetFullPath(SourceFolder));

            foreach (var folder in scanner.Scan(_designModelFolders))
            {
                foreach (var file in Directory.EnumerateFiles(folder, "*.xml", SearchOption.AllDirectories))
                {
                    var relativePath = PathUtils.GetRelativePath(SourceFolder, file);

                    yield return new ProjectFile(this, relativePath);
                }
            }
        }

        public IProjectFile GetFile(string path)
        {
            return new ProjectFile(this, path);
        }

        public IProjectFolder GetFolder(string path)
        {
            return new ProjectFolder(this, path);
        }
        
        #region Equality comparison
        
        private bool Equals(IProject other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Project) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        #endregion
    }
}
