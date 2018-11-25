using Polygen.Core.DataType;
using Polygen.Core.DesignModel;
using Polygen.Core.NamingConvention;
using Polygen.Core.Utils;
using Polygen.Plugins.Base;

namespace Polygen.Plugins.NHibernate.NamingConvention
{
    public class NHibernateNamingConvention : IDatabaseNamingConvention
    {
        public string Language => NHibernatePluginConstants.Language_SQL;

        public string GetTableName(string srcName, string prefix)
        {
            prefix = prefix ?? "ext_";
            
            return prefix + StringUtils.CamelCaseToSeparated(srcName, "_");
        }

        public string GetPrimaryKeyName(string srcName)
        {
            return "pk_" + StringUtils.CamelCaseToSeparated(srcName, "_");
        }

        public string GetColumnName(string srcName)
        {
            return StringUtils.CamelCaseToSeparated(srcName, "_");
        }

        public string GetForeignKeyName(string srcName)
        {
            return "fk_" + StringUtils.CamelCaseToSeparated(srcName, "_") + "_id";
        }

        public string GetTypeName(IDataType dataType)
        {
            return dataType.Name; // TODO: Convert the supported types.
        }
    }
}
