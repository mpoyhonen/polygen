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
            this.Name = name;
            this.Type = type;
            this.SourceFolder = sourceFolder;
            this.ProjectFolder = new ProjectFolder(this, "");
            this.ProjectFile = new ProjectFile(this, this.Name + ".csproj");
        }

        public string Name { get; }
        public string SourceFolder { get; }
        public string TempFolder { get; private set; }
        public string Type { get; }
        public IProjectFile ProjectFile { get; set; }
        public IProjectFolder ProjectFolder { get; }
        public bool IsVisualStudioProject { get; set; }
        public IEnumerable<IProjectFile> DesignModelFiles => this.GetDesignModelFiles();

        public void SetTempFolder(string tempFolder)
        {
            this.TempFolder = tempFolder;
        }

        public void SetDesignModelsFolder(string folders)
        {
            this._designModelFolders = folders;
        }

        private IEnumerable<IProjectFile> GetDesignModelFiles()
        {
            if (string.IsNullOrWhiteSpace(this._designModelFolders))
            {
                yield break;
            }

            if (string.IsNullOrWhiteSpace(this.SourceFolder))
            {
                throw new ConfigurationException("Source folder must be set.");
            }

            var scanner = new FolderScanner(Path.GetFullPath(this.SourceFolder));

            foreach (var folder in scanner.Scan(this._designModelFolders))
            {
                foreach (var file in Directory.EnumerateFiles(folder, "*.xml", SearchOption.AllDirectories))
                {
                    var relativePath = PathUtils.GetRelativePath(this.SourceFolder, file);

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
    }
}
