using HandlebarsDotNet;

namespace Polygen.Templates.HandlebarsNet
{
    public class NoopTextEncoder : ITextEncoder
    {
        public string Encode(string value)
        {
            return value;
        }
    }
}