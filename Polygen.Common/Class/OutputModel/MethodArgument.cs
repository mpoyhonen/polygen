using Polygen.Core.Utils;
using System.Collections.Generic;

namespace Polygen.Common.Class.OutputModel
{
    public class MethodArgument
    {
        private readonly LazyList<Attribute> _attributeList = new LazyList<Attribute>();

        public List<Attribute> Attributes => _attributeList.Value;
        public TypeRef Type { get; set; }
        public string Name { get; set; }
    }
}
