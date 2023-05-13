using System.Data.Common;
using System.Data;
using Testcontainers.MsSql;
using Microsoft.Data.SqlClient;

namespace TestContainersSampleTest
{
    public class MsSqlTest
    {
        private MsSqlContainer _msSqlContainer;

        [SetUp]
        public Task Setup()
        {
            _msSqlContainer = new MsSqlBuilder().Build();
            return _msSqlContainer.StartAsync();
        }

        [TearDown]
        public Task TearDown() 
        {
            return _msSqlContainer.DisposeAsync().AsTask();
        }

        [Test]
        public void ConnectionStateReturnsOpen()
        {
            // Given
            using DbConnection connection = new SqlConnection(_msSqlContainer.GetConnectionString());

            // When
            connection.Open();

            // Then
            Assert.That(connection.State, Is.EqualTo(ConnectionState.Open));
        }

        [Test]
        public async Task ExecScriptReturnsSuccessful()
        {
            // Given
            const string scriptContent = "SELECT 1;";

            // When
            var execResult = await _msSqlContainer.ExecScriptAsync(scriptContent)
                .ConfigureAwait(false);

            // When
            Assert.True(0L.Equals(execResult.ExitCode), execResult.Stderr);
        }
    }
}