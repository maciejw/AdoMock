using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace AdoMocks
{
    [System.ComponentModel.DesignerCategory("code")]
    public partial class DbConnectionMock : DbConnection, IDbConnectionMock, IDisposable
    {
        private IAdoMockTestContext adoMockTestContext;

        public DbConnectionMock(IAdoMockTestContext adoMockContext, string connectionString)
        {
            this.adoMockTestContext = adoMockContext;
            this.ConnectionString = connectionString;


            this.adoMockTestContext.DbConnectionCreated(this);
        }

        public override string ConnectionString { get; set; }


        public override void Open()
        {
            openedCount++;
        }

        public override void Close()
        {
            closedCount++;
        }

        protected override DbCommand CreateDbCommand()
        {
            var command = new DbCommandMock(adoMockTestContext);
            commandCreated.Add(command);

            return command;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }

        int openedCount;
        int IDbConnectionMock.OpenedCount
        {
            get
            {
                return openedCount;
            }
        }

        int closedCount;
        int IDbConnectionMock.ClosedCount
        {
            get
            {
                return closedCount;
            }
        }
        List<IDbCommandMock> commandCreated = new List<IDbCommandMock>();
        List<IDbCommandMock> IDbConnectionMock.CommandCreated
        {
            get
            {
                return commandCreated;
            }
        }
    }

}
