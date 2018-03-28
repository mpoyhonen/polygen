using Polygen.Core.DesignModel;

namespace Polygen.Core.NamingConvention
{
    /// <summary>
    /// Provides methods for creating names in a generated class. 
    /// The names include the namespace, class, method and property names.
    /// </summary>
    public interface IClassNamingConvention
    {
        /// <summary>
        /// Language this naming convention is used for.
        /// </summary>
        string Language { get; }
        /// <summary>
        /// Returns the output folder path for this class.
        /// </summary>
        /// <param name="classNamespace"></param>
        /// <returns></returns>
        string GetOutputFolderPath(INamespace classNamespace);
        /// <summary>
        /// Returns the class namespace name.
        /// </summary>
        /// <param name="classNamespace"></param>
        /// <returns></returns>
        string GetNamespaceName(INamespace classNamespace);
        /// <summary>
        /// Returns class name.
        /// </summary>
        /// <param name="className"></param>
        /// <param name="isInterface"></param>
        /// <returns></returns>
        string GetClassName(string className, bool isInterface);
        /// <summary>
        /// Returns method name.
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        string GetMethodName(string methodName);
        /// <summary>
        /// Return property name.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        string GetPropertyName(string propertyName);
    }
}
