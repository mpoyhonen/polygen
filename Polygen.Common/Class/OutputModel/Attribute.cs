using Polygen.Core.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Polygen.Common.Class.OutputModel
{
    /// <summary>
    /// Defines an attribute which is attached to class, method, property or method arguments.
    /// In C# is is rendered as [Name] or [Name="Value"]
    /// </summary>
    [DebuggerDisplay("Attribute: {Name}")]
    public class Attribute
    {
        private readonly LazyList<ValueTuple<string, object>> _attributeList = new LazyList<ValueTuple<string, object>>();

        public Attribute(string name, params ValueTuple<string, object>[] arguments)
        {
            this.Name = name;

            if (arguments != null)
            {
                this.Arguments.AddRange(arguments);
            }
        }

        public string Name { get; set; }
        public List<ValueTuple<string, object>> Arguments => this._attributeList.Value;
    }
}
