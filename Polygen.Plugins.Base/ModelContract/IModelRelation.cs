
using System.Collections.Generic;

namespace Polygen.Plugins.Base.ModelContract
{
    public interface IModelRelation
    {
        string Name { get; }
        IModel Source { get; }
        IModel Destination { get; }
        
        IEnumerable<IAttribute> Attributes { get; }
        
        void CopyFrom(IModelRelation source);
    }
}