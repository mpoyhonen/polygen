using Polygen.Core.Utils;
using System.Collections.Generic;

namespace Polygen.Common.Class.OutputModel
{
    public class Property
    {
        private readonly LazyList<Attribute> _attributeList = new LazyList<Attribute>();

        public Property(string name, string type, string defaultValue = null)
        {
            Name = name;
            Type = new TypeRef(type);
            DefaultValue = defaultValue;
        }

        public List<Attribute> Attributes => _attributeList.Value;
        public List<string> Modifiers { get; } = new List<string>();
        public string Name { get; set; }
        public TypeRef Type { get; set; }
        public string DefaultValue { get; set; }

        public string ModifiersString => string.Join(" ", Modifiers);
    }
}
