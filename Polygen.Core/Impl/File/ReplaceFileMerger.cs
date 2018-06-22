using System.IO;
using Polygen.Core.File;

namespace Polygen.Core.Impl.File
{
    /// <summary>
    /// This filemerger only always copys the source to the destination
    /// </summary>
    public class ReplaceFileMerger: IFileMerger
    {
        /// <summary>
        /// Copy the source to the destination 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public void Merge(FileInfo source, FileInfo destination)
        {
            source.CopyTo(destination.FullName, true);
        }
    }
}
