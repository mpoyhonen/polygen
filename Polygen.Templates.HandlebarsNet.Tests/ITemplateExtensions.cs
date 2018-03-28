using Polygen.Core.Template;
using System.IO;

namespace Polygen.Templates.HandlebarsNet.Tests
{
    public static class ITemplateExtensions
    {
        public static string RenderIntoString(this ITemplate template, object data)
        {
            using (var writer = new StringWriter())
            {
                ((Template)template).GetRenderFn()(writer, data);
                writer.Flush();

                return writer.ToString();
            }
        }
    }
}
