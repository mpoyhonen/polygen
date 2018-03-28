using System;
using System.IO;

namespace Polygen.Core.Utils
{
    public static class PathUtils
    {
        public static string GetRelativePath(string root, string path)
        {
            root = Path.GetFullPath(root);
            path = Path.GetFullPath(path);

            if (!path.StartsWith(path))
            {
                throw new ArgumentException($"Cannot get relative path from '{path}' with root path '{root}'");
            }

            return path.Substring(root.Length).TrimStart('\\', '/');
        }
    }
}
