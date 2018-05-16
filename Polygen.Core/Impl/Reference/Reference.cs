using Polygen.Core.Parser;
using Polygen.Core.Reference;

namespace Polygen.Core.Impl.Reference
{
    /// <summary>
    /// Base class for reference implementations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReferenceBase<T>: IReference<T>
    {
        public ReferenceBase(string name, string type, IParseLocationInfo parseLocation)
        {
            Name = name;
            Type = type;
            ParseLocation = parseLocation;
        }
        
        /// <inheritdoc />
        public string Name { get; set; }
        
        /// <inheritdoc />
        public string Type { get; set; }

        /// <inheritdoc />
        public T Target { get; set; }

        /// <inheritdoc />
        public IParseLocationInfo ParseLocation { get; set; }
    }
}