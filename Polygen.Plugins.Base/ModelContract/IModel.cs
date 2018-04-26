using System.Collections.Generic;

namespace Polygen.Plugins.Base.ModelContract
{
    public interface IModel
    {
        string Name { get; }
        
        IEnumerable<IAttribute> Attributes { get; }
        IEnumerable<IModelRelation> IncomingRelations { get; }
        IEnumerable<IModelRelation> OutgoingRelations { get; }

        void CopyFrom(IModel source);
        void CopyRelations(IEnumerable<(IModelRelation, IModel)> relationData);
    }
}