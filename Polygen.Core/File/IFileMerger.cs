using System.IO;

namespace Polygen.Core.File
{
    /// <summary>
    /// Describes the FileMerger interface
    /// Used to merge the new generated file with an existing file
    /// </summary>
    public interface IFileMerger
    {
        /// <summary>
        /// Merge the src content into de destination file
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dest"></param>
        void Merge(FileInfo source, FileInfo destination);
    }
}
