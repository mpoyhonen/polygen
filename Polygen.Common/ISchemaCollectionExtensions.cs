using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polygen.Core.Schema;

namespace Polygen.Common
{
    /// <summary>
    /// Helper methods for the common schemas.
    /// </summary>
    public static class ISchemaCollectionExtensions
    {
        public static ISchema DesingModels(this ISchemaCollection collection)
        {
            return collection.GetSchemaByName(Constants.SchemaName_DesignModels);
        }

        public static ISchema ProjectConfiguration(this ISchemaCollection collection)
        {
            return collection.GetSchemaByName(Constants.SchemaName_ProjectConfiguration);
        }
    }
}
