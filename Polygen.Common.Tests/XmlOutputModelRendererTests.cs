using Polygen.Common.Xml;
using Polygen.Core.Impl.DesignModel;
using FluentAssertions;
using System.IO;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace Polygen.Common.Tests
{
    public class XmlOutputModelRendererTests
    {
        [Fact]
        public void Generate_XML_from_output_model()
        {
            var ns = (XNamespace)"urn:test-ns";
            var element = new XElement(ns + "root",
                new XElement(ns + "child", new XAttribute("a", "testing")));
            var outputModel = new XmlOutputModel(element, new Namespace(null), null);
            var renderer = new XmlOutputModelRenderer();
            var xml = default(string);

            using (var s = new MemoryStream())
            using (var writer = new StreamWriter(s))
            {
                renderer.Render(outputModel, writer);
                s.Flush();
                xml = Encoding.UTF8.GetString(s.ToArray());
            }

            var expected = @"
<root xmlns='urn:test-ns'>
  <child a='testing' />
</root>
            ".Trim().Replace("'", "\"");

            xml.Trim().ShouldBeEquivalentTo(expected);
        }
    }
}
