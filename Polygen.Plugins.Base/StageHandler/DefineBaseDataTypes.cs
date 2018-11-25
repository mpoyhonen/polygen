using System;
using System.Collections.Generic;
using System.Text;
using Polygen.Core.DataType;
using Polygen.Core.Impl.DataType;
using Polygen.Core.Stage;

namespace Polygen.Plugins.Base.StageHandler
{
    /// <summary>
    /// Defines all common data types.
    /// </summary>
    public class DefineBaseDataTypes : StageHandlerBase
    {
        public DefineBaseDataTypes(): base(StageType.Initialize, "Base")
        {
        }

        public IDataTypeRegistry DataTypes { get; set; } 

        public override void Execute()
        {
            // TODO: Find out the proper XSD data types!
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_bool, "xs:boolean", "Boolean value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_byte, "byte", "Unsigned 8-bit integer value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_char, "char", "Single character value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_decimal, "decimal", "Decimal floating point value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_double, "double", "Double floating point value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_float, "float", "Float floating point value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_int, "int", "Signed 32-bit integer value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_long, "long", "Signed 64-bit integer value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_sbyte, "sbyte", "Unsigned 8-bit integer value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_short, "short", "Signed 16-bit integer value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_uint, "uint", "Unsigned 32-bit integer value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_ulong, "ulong", "Unsigned 64-bit integer value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_ushort, "ushort", "Unsigned 16-bit integer value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_string, "xs:string", "String value"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_date, "date", "Value which defines a date without a time component"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_time, "time", "Value which defines a time"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_datetime, "datetime", "Value which contains both UTC date and time components"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_guid, "guid", "Globally unique identifier"));
        }
    }
}
