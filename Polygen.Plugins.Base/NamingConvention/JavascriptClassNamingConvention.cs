using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.NamingConvention;
using Polygen.Core.Utils;

namespace Polygen.Plugins.Base.NamingConvention
{
    public class JavascriptClassNamingConvention : IClassNamingConvention
    {
        public string Language => BasePluginConstants.Language_Javascript;
       
        public string GetClassName(string className, bool isInterface)
        {
            return (isInterface ? "I" : "") + className;
        }

        public string GetMethodName(string methodName)
        {
            return StringUtils.Uncapitalize(methodName);
        }

        public string GetNamespaceName(INamespace classNamespace)
        {
            var name = classNamespace.Name;
            var parts = name.Split('.');

            for (var i = 1; i < parts.Length; i++)
            {
                parts[i] = parts[i].ToLowerInvariant();
            }

            return string.Join(".", parts);
        }

        public string GetPropertyName(string propertyName)
        {
            return StringUtils.Uncapitalize(propertyName);
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
