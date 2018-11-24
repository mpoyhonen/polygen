using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygen.Core.Tests
{
    public class TempFolder: IDisposable
    {
        private string _tempFolder;
        private string _baseFolder;

        public TempFolder(string baseFolderName = "Polygen")
        {
            _baseFolder = Path.Combine(Path.GetTempPath(), baseFolderName);
        }

        public string GetRootPath()
        {
            if (_tempFolder == null)
            {
                _tempFolder = Path.Combine(_baseFolder, Guid.NewGuid().ToString().Replace("-", ""));
                Directory.CreateDirectory(_tempFolder);
            }

            return _tempFolder;
        }

        public string GetPath(string path)
        {
            return Path.GetFullPath(Path.Combine(GetRootPath(), path));
        }

        public void Dispose()
        {
            if (_tempFolder != null)
            {
                Directory.Delete(_tempFolder, true);
                _tempFolder = null;
            }
        }

        public string CreateFolder(string dirPath)
        {
            dirPath = GetPath(dirPath);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            return dirPath;
        }

        public TextWriter OpenTextFileForWriting(string filePath)
        {
            filePath = GetPath(filePath);

            var dirPath = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrWhiteSpace(dirPath) && !Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            return new StreamWriter(System.IO.File.Open(filePath, FileMode.Create), Encoding.UTF8);
        }

        public void CreateWriteTextFile(string filePath, string text)
        {
            using (var writer = OpenTextFileForWriting(filePath))
            {
                writer.Write(text);
            }
        }

        // https://stackoverflow.com/questions/58744/copy-the-entire-contents-of-a-directory-in-c-sharp
        public void CopyFrom(string sourceDir)
        {
            void CopyAll(DirectoryInfo source, DirectoryInfo target)
            {
                Directory.CreateDirectory(target.FullName);

                // Copy each file into the new directory.
                foreach (var fi in source.GetFiles())
                {
                    fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                }

                // Copy each subdirectory using recursion.
                foreach (var diSourceSubDir in source.GetDirectories())
                {
                    var nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);

                    CopyAll(diSourceSubDir, nextTargetSubDir);
                }
            }

            CopyAll(new DirectoryInfo(sourceDir), new DirectoryInfo(GetRootPath()));
        }
    }
}
