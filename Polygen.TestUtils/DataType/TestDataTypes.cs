using Polygen.Core.DataType;
using Polygen.Core.Impl.DataType;

namespace Polygen.TestUtils.DataType
{
    public class TestDataTypes
    {
        public static IDataType String = new PrimitiveType("string", "xs:string", "string value");
        public static IDataType Int = new PrimitiveType("int", "xs:int", "int value");
    }
}
