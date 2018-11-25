using System;
using Polygen.Core.File;
using Polygen.Core.OutputModel;

namespace Polygen.Core.Impl.File
{
    public class FileMergerFactory
    {
        /// <summary>
        /// Return the FileMerger to use based on the merge mode
        /// </summary>
        /// <param name="outputModelMergeMode"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IFileMerger GetForMode(OutputModelMergeMode outputModelMergeMode)
        {
            switch (outputModelMergeMode)
            {
                case OutputModelMergeMode.Replace:
                    return new ReplaceFileMerger();
                case OutputModelMergeMode.Skip:
                    return new SkipFileMerger();
                case OutputModelMergeMode.MergeUpdateRegions:
                    throw new NotImplementedException($"File merger for mode {OutputModelMergeMode.MergeUpdateRegions} is not yet implemented");
                case OutputModelMergeMode.MergePreserveRegions:
                    throw new NotImplementedException($"File merger for mode {OutputModelMergeMode.MergePreserveRegions} is not yet implemented");
                default:
                    throw new ArgumentOutOfRangeException(nameof(outputModelMergeMode), outputModelMergeMode, null);
            }
        }
    }
}
