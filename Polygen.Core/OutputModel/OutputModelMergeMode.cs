namespace Polygen.Core.OutputModel
{
    /// <summary>
    /// Defines how output model file copying will be handled when the destination
    /// file already exists in the project.
    /// </summary>
    public enum OutputModelMergeMode
    {
        /// <summary>
        /// Replaces the project file.
        /// </summary>
        Replace,
        /// <summary>
        /// Project file will be left untouched.
        /// </summary>
        Skip,
        /// <summary>
        /// Merges the two files by replacing contents of special regions in the project file.
        /// </summary>
        MergeUpdateRegions,
        /// <summary>
        /// Merges the two files by preserving contents of special regions in the project file.
        /// Other parts of the project file are replaced from the generated file.
        /// </summary>
        MergePreserveRegions
    }
}
