using System.IO;
using System.Text;

namespace Polygen.Templates.HandlebarsNet
{
    /// <summary>
    /// Provides access to the template text, either directly from a string or from a file.
    /// </summary>
    public class TemplateSource
    {
        public bool IsFile => FilePath != null;
        public string FilePath { get; private set; }
        public string Text { get; private set; }

        public static TemplateSource CreateForText(string text)
        {
            return new TemplateSource()
            {
                Text = text
            };
        }

        public static TemplateSource CreateForFile(string filePath)
        {
            return new TemplateSource()
            {
                FilePath = filePath
            };
        }
    }
}
