using Polygen.Core.Parser;
using Polygen.Core.Reference;

namespace Polygen.Core.DesignModel
{
    /// <summary>
    /// Helper extension methods for this DesignModel namespace.
    /// </summary>
    public static class Extensions
    {
        public static DesignModelReference CreateDesignModelReference(this IDesignModelCollection designModelCollection,
            INamespace ns,
            IParseLocationInfo parseLocation,
            string name, string type = null)
        {
            var pos = name.IndexOf(':');

            if (pos != -1)
            {
                name = name.Substring(0, pos);
                type = name.Substring(pos + 1);
            }

            pos = name.LastIndexOf('.');

            if (pos == -1)
            {
                return new DesignModelReference(name, ns, type, parseLocation);
            }
            
            var nsName = name.Substring(0, pos);

            name = name.Substring(pos + 1);
            ns = designModelCollection.GetNamespace(nsName);

            return new DesignModelReference(name, ns, type, parseLocation);
        }
    }
}