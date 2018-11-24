using System.Xml.Linq;

namespace Polygen.Plugins.Base
{
    public static class BasePluginConstants
    {
        public const string DesignModel_SchemaName = "DesignModel";
        public readonly static XNamespace DesignModel_SchemaNamespace = "uri:polygen/1.0/designmodel";

        #region Data types
        public const string DataType_bool = "bool";
        public const string DataType_byte = "byte";
        public const string DataType_char = "char";
        public const string DataType_decimal = "decimal";
        public const string DataType_double = "double";
        public const string DataType_float = "float";
        public const string DataType_int = "int";
        public const string DataType_long = "long";
        public const string DataType_sbyte = "sbyte";
        public const string DataType_short = "short";
        public const string DataType_uint = "uint";
        public const string DataType_ulong = "ulong";
        public const string DataType_ushort = "ushort";
        public const string DataType_string = "string";
        public const string DataType_date = "date";
        public const string DataType_time = "time";
        public const string DataType_datetime = "datetime";
        public const string DataType_guid = "guid";
        #endregion

        #region Project types
        public const string ProjectType_Design = "Design";
        public const string ProjectType_Data = "Data";
        public const string ProjectType_Business = "Business";
        public const string ProjectType_Web = "Web";
        #endregion region

        #region Desing model types
        public const string DesignModelType_Entity = "Entity";
        #endregion


        public const string OutputModelName_SchemaXSD = "SchemaXSD";

        public const string Language_CSharp = "csharp";
        public const string Language_Javascript = "javascript";
        public const string Language_XML = "xml";
    }
}
