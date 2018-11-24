using Polygen.Core.Project;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace Polygen.Core.Impl.Project
{
    [DebuggerDisplay("ProjectFile: Source: {_sourcePath}, Temp: {_tempPath}")]
    public class ProjectFile : ProjectFileSystemEntryBase, IProjectFile
    {
        public ProjectFile(string path) : base(path, false)
        {
        }

        public ProjectFile(IProject project, string relativePath) : base(project, relativePath, false)
        {
        }

        public TextReader OpenAsTextForReading()
        {
            return new StreamReader(File.Open(GetSourcePath(true), FileMode.Open), Encoding.UTF8);
        }

        public TextWriter OpenAsTextForWriting()
        {
            var path = GetTempPath(true);
            var dir = Path.GetDirectoryName(path);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            return new StreamWriter(File.Open(path, FileMode.Create), Encoding.UTF8);
        }

        public string ReadText()
        {
            using (var reader = OpenAsTextForReading())
            {
                return reader.ReadToEnd();
            }
        }

        public void WriteText(string text)
        {
            using (var writer = OpenAsTextForWriting())
            {
                writer.Write(text);
            }
        }
    }
}
