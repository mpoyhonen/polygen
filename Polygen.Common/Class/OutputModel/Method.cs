using Polygen.Core.Utils;
using System.Collections.Generic;

namespace Polygen.Common.Class.OutputModel
{
    public class Method
    {
        private readonly LazyList<Attribute> _attributeList = new LazyList<Attribute>();
        private readonly LazyList<string> _modifierList = new LazyList<string>();
        private readonly LazyList<MethodArgument> _argumentList = new LazyList<MethodArgument>();

        public List<Attribute> Attributes => this._attributeList.Value;
        public List<string> Modifiers => this._modifierList.Value;
        public string Name { get; set; }
        public TypeRef ReturnType { get; set; }
        public List<MethodArgument> Arguments => this._argumentList.Value;
    }
}
