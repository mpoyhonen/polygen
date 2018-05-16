using Polygen.Core.Impl.Reference;
using Polygen.Core.Parser;

namespace Polygen.Core.DesignModel
{
    /// <summary>
    /// Contains a reference to a design model. This allows us to point to a design model by name (and possibly
    /// type) during design model parsing.
    /// </summary>
    public class DesignModelReference<T>: ReferenceBase<T>
    {
        public DesignModelReference(string name, INamespace ns, string type, IParseLocationInfo parseLocation = null)
            : base($"{ns.Name}.{name}", type, parseLocation)
        {
            LocalName = name;
            Namespace = ns;
        }
        
        /// <summary>
        /// Design model name within a namespace.
        /// </summary>
        public string LocalName { get; set; }

        /// <summary>
        /// Design model namespace.
        /// </summary>
        public INamespace Namespace { get; set; }
    }

    /// <summary>
    /// Contains a reference to a design model. This allows us to point to a design model by name (and possibly
    /// type) during design model parsing.
    /// </summary>
    public class DesignModelReference : DesignModelReference<IDesignModel>
    {
        public DesignModelReference(string name, INamespace ns, string type, IParseLocationInfo parseLocation = null)
            : base(name, ns, type, parseLocation)
        {
        }
    }
}