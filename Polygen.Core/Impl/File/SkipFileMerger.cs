using System.IO;
using Polygen.Core.File;

namespace Polygen.Core.Impl.File
{
    /// <summary>
    /// This filemerger only creates the file if it doesn't exist
    /// otherwise it is skipped    
    /// </summary>
    public class SkipFileMerger: IFileMerger
    {
        /// <summary>
        /// Copy the source to the destination if it doesn't exist
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void Merge(FileInfo source, FileInfo destination)
        {
            if (!destination.Exists)
            {
                source.CopyTo(destination.FullName);
            }
        }
    }
}
