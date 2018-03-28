using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polygen.Core.Utils
{
    /// <summary>
    /// Returns all folders matching with pattern. Wildcard folders can be given with '*'.
    /// Multiple patterns can be separated with a comma.
    /// </summary>
    public class FolderScanner
    {
        private readonly string _rootFolder;

        public FolderScanner(string rootFolder)
        {
            this._rootFolder = rootFolder;
        }

        public IEnumerable<string> Scan(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
            {
                return Enumerable.Empty<string>();
            }

            var res = new List<string>();

            foreach (var p in pattern.Split(',').Select(x => x.Trim()).Where(x => x.Length > 0))
            {
                this.ExpandFolderPath(Path.GetFullPath(this._rootFolder), p, res);
            }

            return res;
        }

        private void ExpandFolderPath(string parentPath, string path, List<string> res)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                if (Directory.Exists(parentPath))
                {
                    res.Add(parentPath);
                }

                return;
            }

            var parts = path.Split(new[] { '/', '\\' }, 2);
            var folderName = parts[0];
            var rest = parts.Length > 1 ? parts[1] : null;

            if (folderName == "*")
            {
                if (Directory.Exists(parentPath))
                {
                    foreach (var childFolder in Directory.EnumerateDirectories(parentPath))
                    {
                        this.ExpandFolderPath(Path.Combine(parentPath, childFolder), rest, res);
                    }
                }
            }
            else
            {
                this.ExpandFolderPath(Path.Combine(parentPath, folderName), rest, res);
            }
        }
    }
}
