using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace Ado
{
    public class DbConnectionFactory
    {
        public static Func<string, DbConnection> Create { get; set; } = connectionString => new System.Data.SqlClient.SqlConnection(connectionString);
    }

}
