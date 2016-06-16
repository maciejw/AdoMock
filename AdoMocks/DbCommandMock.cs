using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace AdoMocks
{
    [System.ComponentModel.DesignerCategory("code")]
    public partial class DbCommandMock : IDbCommandMock
    {
        private IAdoMockTestContext adoMockTestContext;

        public DbCommandMock(IAdoMockTestContext adoMockTestContext)
        {
            this.adoMockTestContext = adoMockTestContext;

            DbParameterCollection = new DbParameterCollectionMock(adoMockTestContext);

            createdParameters = new List<IDbParameterMock>();
        }

        public override CommandType CommandType { get; set; }

        public override string CommandText { get; set; }

        protected override DbParameterCollection DbParameterCollection { get; }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            return adoMockTestContext.GetExecuteDbDataReaderResult(behavior);
        }

        public override int ExecuteNonQuery()
        {
            return adoMockTestContext.GetExecuteNonQueryResult();
        }

        List<IDbParameterMock> createdParameters;

        List<IDbParameterMock> IDbCommandMock.CreatedParameters
        {
            get
            {
                return createdParameters;
            }
        }

        DbCommand IDbCommandMock.Object
        {
            get
            {
                return this;
            }
        }

        protected override DbParameter CreateDbParameter()
        {
            var dbParameter = new DbParameterMock(adoMockTestContext);

            createdParameters.Add(dbParameter);

            return dbParameter;
        }
    }

}
