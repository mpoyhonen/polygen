using System;
using System.Collections.Generic;
using System.IO;

namespace Polygen.Core.Template
{
    /// <summary>
    /// Represents a renderable template.
    /// </summary>
    public interface ITemplate
    {
        /// <summary>
        /// Template name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Template type. This determines the renderer type.
        /// </summary>
        string Type { get; }
        /// <summary>
        /// Returns template text.
        /// </summary>
        /// <returns></returns>
        string GetTemplateText();
        /// <summary>
        /// Renders the template using given data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="writer"></param>
        void Render(Dictionary<string, object> data, TextWriter writer);
    }
}
