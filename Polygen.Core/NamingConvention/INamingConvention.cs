using Polygen.Core.DataType;
using Polygen.Core.DesignModel;

namespace Polygen.Core.NamingConvention
{
    /// <summary>
    /// Base interface for a naming convention.
    /// The names include the namespace, class, method and property names.
    /// </summary>
    public interface INamingConvention
    {
        /// <summary>
        /// Language this naming convention is used for.
        /// </summary>
        string Language { get; }
    }
}
