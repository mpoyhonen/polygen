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
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_bool, "xs:boolean"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_byte, "byte"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_char, "char"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_decimal, "decimal"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_double, "double"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_float, "float"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_int, "int"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_long, "long"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_sbyte, "sbyte"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_short, "short"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_uint, "uint"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_ulong, "ulong"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_ushort, "ushort"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_string, "xs:string"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_date, "date"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_time, "time"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_datetime, "datetime"));
            DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_guid, "guid"));
        }
    }
}
