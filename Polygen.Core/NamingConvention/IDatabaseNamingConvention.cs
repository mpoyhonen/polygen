using Polygen.Core.DataType;
using Polygen.Core.DesignModel;

namespace Polygen.Core.NamingConvention
{
    /// <summary>
    /// Provides methods for creating names for database tables, etc. 
    /// </summary>
    public interface IDatabaseNamingConvention: INamingConvention
    {
        /// <summary>
        /// Return name of a table to be used for the generated SQL.
        /// </summary>
        /// <param name="srcName">Source name, e.g. entity name</param>
        /// <param name="prefix">Optional table prefix</param>
        /// <returns></returns>
        string GetTableName(string srcName, string prefix = null);
        /// <summary>
        /// Return name of a column to be used for the generated SQL.
        /// </summary>
        /// <param name="srcName">Source name, e.g. entity ID property name</param>
        /// <returns></returns>
        string GetPrimaryKeyName(string srcName);
        /// <summary>
        /// Return name of a column to be used for the generated SQL.
        /// </summary>
        /// <param name="srcName">Source name, e.g. entity property name</param>
        /// <returns></returns>
        string GetColumnName(string srcName);
        /// <summary>
        /// Return name of a column to be used for the generated SQL.
        /// </summary>
        /// <param name="srcName">Source name, e.g. entity relation name</param>
        /// <returns></returns>
        string GetForeignKeyName(string srcName);
        /// <summary>
        /// Returns data type name used for the generated SQL.
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        string GetTypeName(IDataType dataType);
    }
}
