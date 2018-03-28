using Polygen.Core.DesignModel;
using Polygen.Core.NamingConvention;

namespace Polygen.TestUtils.NamingConvention
{
    public class TestClassNamingConvention : IClassNamingConvention
    {
        public string Language => "test";

        public string GetClassName(string className, bool isInterface)
        {
            return className;
        }

        public string GetMethodName(string methodName)
        {
            return methodName;
        }

        public string GetNamespaceName(INamespace classNamespace)
        {
            return classNamespace.Name;
        }

        public string GetOutputFolderPath(INamespace classNamespace)
        {
            return classNamespace.Name.Replace(".", "/");
        }

        public string GetPropertyName(string propertyName)
        {
            return propertyName;
        }
    }
}
