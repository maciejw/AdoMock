using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AdoMocks.Tests
{
    public class DbConnectionTests
    {
        [Fact]
        void should_set_dbconnection_mock()
        {
            var context = new AdoMockContext();

            var connection = Ado.DbConnectionFactory.Create("test connection string");

            Assert.Equal("test connection string", connection.ConnectionString);

            Assert.Equal(1, context.VerificationContext.ConnectionsCreated.Count);
            Assert.Same(context.VerificationContext.ConnectionsCreated[0], connection);
        }

        [Fact]
        void should_count_open()
        {
            var context = new AdoMockContext();

            var connection = Ado.DbConnectionFactory.Create("test connection string");

            connection.Open();

            Assert.Equal(1, context.VerificationContext.ConnectionsCreated[0].OpenedCount);

        }

        [Fact]
        void should_count_close()
        {
            var context = new AdoMockContext();

            var connection = Ado.DbConnectionFactory.Create("test connection string");

            connection.Close();

            Assert.Equal(1, context.VerificationContext.ConnectionsCreated[0].ClosedCount);

        }

        [Fact]
        void should_count_dispose_tha_same_as_close()
        {
            var context = new AdoMockContext();

            var connection = Ado.DbConnectionFactory.Create("test connection string");

            connection.Dispose();

            Assert.Equal(1, context.VerificationContext.ConnectionsCreated[0].ClosedCount);

        }

        [Fact]
        void should_count_create_commnad()
        {
            var context = new AdoMockContext();

            var connection = Ado.DbConnectionFactory.Create("test connection string");


            var command = connection.CreateCommand();

            Assert.Equal(1, context.VerificationContext.ConnectionsCreated[0].CommandCreated.Count);
            Assert.Same(command, context.VerificationContext.ConnectionsCreated[0].CommandCreated[0]);

        }
    }
}
