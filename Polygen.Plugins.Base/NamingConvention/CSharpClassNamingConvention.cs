using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.NamingConvention;

namespace Polygen.Plugins.Base.NamingConvention
{
    public class CSharpClassNamingConvention : IClassNamingConvention
    {
        public string Language => BasePluginConstants.Language_CSharp;

        public string GetClassName(string className, bool isInterface)
        {
            return (isInterface ? "I" : "") + className;
        }

        public string GetMethodName(string methodName)
        {
            return methodName;
        }

        public string GetNamespaceName(INamespace classNamespace)
        {
            return classNamespace.Name;
        }

        public string GetPropertyName(string propertyName)
        {
            return propertyName;
        }

        public string GetTypeName(IDataType dataType)
        {
            // For now, just use the given name.
            return dataType.Name;
        }

        public string GetOutputFolderPath(INamespace classNamespace)
        {
            return this.GetNamespaceName(classNamespace).Replace(".", "/");
        }
    }
}
