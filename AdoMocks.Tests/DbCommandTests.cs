using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Ado;
using System.Data;

namespace AdoMocks.Tests
{
    public class DbCommandTests
    {
        [Fact]
        void should_be_able_to_set_command_type()
        {
            var context = new AdoMockContext();

            var connection = Ado.DbConnectionFactory.Create("test connection string");


            var command = connection.CreateCommand();

            command.CommandType = System.Data.CommandType.StoredProcedure;

        }
        [Fact]
        void should_be_able_to_set_command_text()
        {
            var context = new AdoMockContext();

            var connection = Ado.DbConnectionFactory.Create("test connection string");

            var command = connection.CreateCommand();

            command.CommandText = "some sql";

        }

        [Fact]
        void should_count_create_parameter()
        {
            var context = new AdoMockContext();

            var connection = Ado.DbConnectionFactory.Create("test connection string");

            var command = connection.CreateCommand();

            var parameter = command.CreateParameter();

            Assert.Equal(1, context.VerificationContext.ConnectionsCreated[0].CommandCreated[0].CreatedParameters.Count);
            Assert.Same(parameter, context.VerificationContext.ConnectionsCreated[0].CommandCreated[0].CreatedParameters[0].Object);


        }

        [Fact]
        void should_add_parameters_to_command()
        {
            var context = new AdoMockContext();

            var connection = Ado.DbConnectionFactory.Create("test connection string");

            var command = connection.CreateCommand();

            command.AddParameter("parameter 1", DbType.AnsiString, 50, "parameter value");
            command.AddParameter("parameter 2", DbType.Boolean, true);
            var command1 = context.VerificationContext.ConnectionsCreated[0].CommandCreated[0];


            Assert.Equal(command1.CreatedParameters.Count, command1.Object.Parameters.Count);

            var parameter1 = command1.CreatedParameters[0].Object;

            Assert.Equal("parameter 1", parameter1.ParameterName);
            Assert.Equal(DbType.AnsiString, parameter1.DbType);
            Assert.Equal(50, parameter1.Size);
            Assert.Equal("parameter value", parameter1.Value);

            var parameter2 = command1.CreatedParameters[1].Object;

            Assert.Equal("parameter 2", parameter2.ParameterName);
            Assert.Equal(DbType.Boolean, parameter2.DbType);
            Assert.Equal(true, parameter2.Value);


        }

        [Fact]
        void should_be_able_to_provide_results_for_dbreader()
        {
            var context = new AdoMockContext();

            context.SetupContext
                .SetupExecuteReaderResult(schema => schema.AddColumn<string>("column1").AddColumn<bool?>("column2"))
                .AddRecord("text", true)
                .AddRecord("text 2", null)
                .RegisterExecuteReaderResult()
                .RegisterExecuteReaderResult(new Exception("test message"));

            var connection = Ado.DbConnectionFactory.Create("test connection string");

            var command = connection.CreateCommand();


            var results = command.ExecuteReader();

            Assert.True(results.Read());
            Assert.Equal("text", results.GetString(results.GetOrdinal("column1")));
            Assert.Equal(true, results.GetBoolean(results.GetOrdinal("column2")));

            Assert.True(results.Read());
            Assert.Equal("text 2", results.GetString(results.GetOrdinal("column1")));
            Assert.Equal(true, results.IsDBNull(results.GetOrdinal("column2")));

            Assert.False(results.Read());

            var exception = Assert.Throws<Exception>(() => command.ExecuteReader());
            Assert.Equal("test message", exception.Message);


        }
        [Fact]
        void should_be_able_to_provide_results_for_execute_non_query()
        {
            var context = new AdoMockContext();

            context.SetupContext
                .RegisterExecuteNonQueryResult(1)
                .RegisterExecuteNonQueryResult(new Exception("test message"));

            var connection = Ado.DbConnectionFactory.Create("test connection string");

            var command = connection.CreateCommand();

            var results = command.ExecuteNonQuery();
            Assert.Equal(1, results);

            var exception = Assert.Throws<Exception>(() => command.ExecuteNonQuery());
            Assert.Equal("test message", exception.Message);


        }
    }
}
