using System;
using Polygen.Core.File;
using Polygen.Core.OutputModel;

namespace Polygen.Core.Impl.File
{
    public class FileMergerFactory
    {
        /// <summary>
        /// Return the FileMerger to use based on the mergemode
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
                    break;
                case OutputModelMergeMode.Skip:
                    return new SkipFileMerger();
                    break;
                case OutputModelMergeMode.MergeUpdateRegions:
                    throw new NotImplementedException($"File merger for mode {OutputModelMergeMode.MergeUpdateRegions} is not yet implemented");
                    break;
                case OutputModelMergeMode.MergePreserveRegions:
                    throw new NotImplementedException($"File merger for mode {OutputModelMergeMode.MergePreserveRegions} is not yet implemented");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(outputModelMergeMode), outputModelMergeMode, null);
            }
        }
    }
}
