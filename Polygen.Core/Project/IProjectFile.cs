using System;
using System.IO;

namespace Polygen.Core.Project
{
    /// <summary>
    /// Represents a filesystem file.
    /// </summary>
    public interface IProjectFile
    {
        /// <summary>
        /// Owning project.
        /// </summary>
        IProject Project { get; }
        /// <summary>
        /// File path relative to the project.
        /// </summary>
        string RelativePath { get; }
        /// <summary>
        /// File name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// File extension.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Opens a UTF-8 text file for reading.
        /// </summary>
        /// <returns></returns>
        TextReader OpenAsTextForReading();
        /// <summary>
        /// Opens a UTF-8 text file for reading.
        /// </summary>
        /// <returns></returns>
        TextWriter OpenAsTextForWriting();
        /// <summary>
        /// Return UTF-8 encoded text file contents.
        /// </summary>
        string ReadText();
        /// <summary>
        /// Writes text file contents as UTF-8 encoded text.
        /// </summary>
        /// <param name="text"></param>
        void WriteText(string text);

        /// <summary>
        /// Returns absolute file path in the project source folder.
        /// </summary>
        /// <param name="throwIfNotSet"></param>
        string GetSourcePath(bool throwIfNotSet = false);
        /// <summary>
        /// Returns absolute file path in the project temp folder.
        /// </summary>
        /// <param name="throwIfNotSet"></param>
        string GetTempPath(bool throwIfNotSet = false);
    }
}