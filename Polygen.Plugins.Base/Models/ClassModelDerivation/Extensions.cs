using Polygen.Core.ClassModel;

namespace Polygen.Plugins.Base.Models.ClassModelDerivation
{
    /// <summary>
    /// Helper extension methods handling ClassModelDerivation data.
    /// </summary>    
    public static class Extensions
    {
        public static ClassDerivation GetClassDerivationData(this IClassModel classModel)
        {
            classModel.CustomData.TryGetValue(nameof(ClassDerivation), out var res);
            return (ClassDerivation) res;
        }
        
        public static void SetClassDerivationData(this IClassModel classModel, ClassDerivation data)
        {
            classModel.CustomData[nameof(ClassDerivation)] = data;
        }
    }
}