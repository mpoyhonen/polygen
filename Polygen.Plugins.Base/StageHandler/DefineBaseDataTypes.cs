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
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_bool, "xs:boolean"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_byte, "byte"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_char, "char"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_decimal, "decimal"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_double, "double"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_float, "float"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_int, "int"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_long, "long"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_sbyte, "sbyte"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_short, "short"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_uint, "uint"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_ulong, "ulong"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_ushort, "ushort"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_string, "xs:string"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_date, "date"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_time, "time"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_datetime, "datetime"));
            this.DataTypes.Add(new PrimitiveType(BasePluginConstants.DataType_guid, "guid"));
        }
    }
}
