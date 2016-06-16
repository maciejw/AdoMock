using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace AdoMocks
{
    public interface IAdoMockTestContext
    {
        void DbConnectionCreated(IDbConnectionMock dbConnectionMock);
        DbDataReader GetExecuteDbDataReaderResult(CommandBehavior behavior);
        int GetExecuteNonQueryResult();
    }

}
