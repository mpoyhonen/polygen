using System.Diagnostics;
using Polygen.Core.Project;

namespace Polygen.Core.Impl.Project
{
    [DebuggerDisplay("ProjectFolder: Source: {_sourcePath}, Temp: {_tempPath}")]
    public class ProjectFolder : ProjectFileSystemEntryBase, IProjectFolder
    {
        public ProjectFolder(string path) : base(path, true)
        {
        }

        public ProjectFolder(IProject project, string relativePath) : base(project, relativePath, true)
        {
        }

        public IProjectFile GetFile(string path)
        {
            return new ProjectFile(Project, $"{RelativePath}/{path}");
        }

        public IProjectFolder GetFolder(string path)
        {
            return new ProjectFolder(Project, $"{RelativePath}/{path}");
        }
    }
}
