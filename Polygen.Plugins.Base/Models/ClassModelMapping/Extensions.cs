using Polygen.Core.ClassModel;

namespace Polygen.Plugins.Base.Models.ClassModelMapping
{
    /// <summary>
    /// Helper extension methods handling ClassMapping data.
    /// </summary>    
    public static class Extensions
    {
        public static ClassMapping GetClassMappingData(this IClassModel classModel)
        {
            classModel.CustomData.TryGetValue(nameof(ClassMapping), out var res);
            return (ClassMapping) res;
        }
        
        public static void SetClassMappingData(this IClassModel classModel, ClassMapping data)
        {
            classModel.CustomData[nameof(ClassMapping)] = data;
        }
    }
}