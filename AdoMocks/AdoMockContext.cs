using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace AdoMocks
{
    public class AdoMockContext : IAdoMockTestContext, IAdoMockSetupContext, IAdoMockVerificationContext, IReaderConfiguration, IRecordConfiguration
    {

        List<Tuple<DbDataReader, Exception>> executeReaderResults = new List<Tuple<DbDataReader, Exception>>();
        List<CommandBehavior> executeReaderResultsCalls = new List<CommandBehavior>();
        int executeNonQueryCalls;
        List<Tuple<int, Exception>> executeNonQueryResults = new List<Tuple<int, Exception>>();

        public AdoMockContext()
        {
            Ado.DbConnectionFactory.Create = connectionString => new DbConnectionMock(this, connectionString);
        }

        void IAdoMockTestContext.DbConnectionCreated(IDbConnectionMock dbConnectionMock)
        {
            VerificationContext.ConnectionsCreated.Add(dbConnectionMock);
        }

        DataTable dataTable;

        IAdoMockSetupContext IAdoMockSetupContext.RegisterExecuteReaderResult(Exception exception)
        {

            executeReaderResults.Add(new Tuple<DbDataReader, Exception>(null, exception));

            return this;
        }

        IRecordConfiguration IAdoMockSetupContext.SetupExecuteReaderResult(Action<IReaderConfiguration> schemaConfiguration)
        {
            dataTable = new DataTable();
            schemaConfiguration(this);
            return this;
        }

        IReaderConfiguration IReaderConfiguration.AddColumn<T>(string name)
        {
            var type = typeof(T);

            var column = this.dataTable.Columns.Add(name);

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                column.AllowDBNull = true;
                column.DataType = type.GetGenericArguments().First();
            }
            else
            {
                column.DataType = type;
            }

            return this;
        }

        IRecordConfiguration IRecordConfiguration.AddRecord(params object[] args)
        {
            dataTable.LoadDataRow(args, true);

            return this;
        }

        IAdoMockSetupContext IRecordConfiguration.RegisterExecuteReaderResult()
        {
            executeReaderResults.Add(new Tuple<DbDataReader, Exception>(dataTable.CreateDataReader(), null));

            return this;
        }

        DbDataReader IAdoMockTestContext.GetExecuteDbDataReaderResult(CommandBehavior behavior)
        {
            var result = executeReaderResults[executeReaderResultsCalls.Count];

            executeReaderResultsCalls.Add(behavior);

            if (result.Item2 == null)
            {
                return result.Item1;
            }
            throw result.Item2;
        }

        int IAdoMockTestContext.GetExecuteNonQueryResult()
        {
            var result = executeNonQueryResults[executeNonQueryCalls];

            executeNonQueryCalls++;

            if (result.Item2 == null)
            {
                return result.Item1;
            }
            throw result.Item2;
        }

        IAdoMockSetupContext IAdoMockSetupContext.RegisterExecuteNonQueryResult(int result)
        {
            executeNonQueryResults.Add(new Tuple<int, Exception>(result, null));

            return this;
        }

        IAdoMockSetupContext IAdoMockSetupContext.RegisterExecuteNonQueryResult(Exception exception)
        {
            executeNonQueryResults.Add(new Tuple<int, Exception>(0, exception));

            return this;
        }

        public IAdoMockSetupContext SetupContext { get { return this; } }
        public IAdoMockVerificationContext VerificationContext { get { return this; } }

        List<IDbConnectionMock> IAdoMockVerificationContext.ConnectionsCreated { get; } = new List<IDbConnectionMock>();
    }

}
