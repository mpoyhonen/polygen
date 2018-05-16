using Polygen.Core.DataType;
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
        /// Return name of a class to be used for the generated code.
        /// </summary>
        /// <param name="className"></param>
        /// <param name="isInterface"></param>
        /// <returns></returns>
        string GetClassName(string className, bool isInterface);
        /// <summary>
        /// Return name of a class method to be used for the generated code.
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        string GetMethodName(string methodName);
        /// <summary>
        /// Return name of a class property to be used for the generated code.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        string GetPropertyName(string propertyName);
        /// <summary>
        /// Returns data type name used for the geneated code.
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        string GetTypeName(IDataType dataType);
    }
}
