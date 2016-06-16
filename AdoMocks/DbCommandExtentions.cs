using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace Ado
{
    public static class DbCommandExtentions
    {

        private static DbParameter AddParameter(this DbCommand @this, string parameterName)
        {
            var parameter = @this.CreateParameter();

            @this.Parameters.Add(parameter);

            parameter.ParameterName = parameterName;
            return parameter;
        }

        public static DbParameter AddParameter(this DbCommand @this, string parameterName, DbType dbType)
        {
            var parameter = @this.AddParameter(parameterName);

            parameter.DbType = dbType;
            return parameter;
        }
        public static DbParameter AddParameter(this DbCommand @this, string parameterName, DbType dbType, int size)
        {
            var parameter = @this.AddParameter(parameterName, dbType);

            parameter.Size = size;
            return parameter;
        }
        public static DbParameter AddParameter(this DbCommand @this, string parameterName, DbType dbType, object value)
        {
            var parameter = @this.AddParameter(parameterName, dbType);

            parameter.Value = value;
            return parameter;
        }
        public static DbParameter AddParameter(this DbCommand @this, string parameterName, DbType dbType, int size, object value)
        {
            var parameter = @this.AddParameter(parameterName, dbType, size);

            parameter.Value = value;
            return parameter;
        }

    }

}
