using System.Data.Common;
using System.Data;
using Testcontainers.MsSql;
using Microsoft.Data.SqlClient;

namespace TestContainersSampleTest
{
    public class MsSqlTest
    {
        private MsSqlContainer _msSqlContainer;
        private string _dataDirName;
        private List<string> _schemas;

        [SetUp]
        public async Task Setup()
        {
            _dataDirName = "Data";
            var dirs = Directory.GetDirectories(".");
            _schemas = new List<string>(dirs.Select(Path.GetFileName));

            _msSqlContainer = new MsSqlBuilder().Build();
            await _msSqlContainer.StartAsync();

            await CreateDatabase();
            await CreateSchemas();
            await SetUpDatabase();
        }

        private async Task CreateDatabase()
        {
            var scripts = new List<string> { GetFileContent(@"CreateDatabase.sql") };

            await Execute(scripts);
        }

        private string GetFileContent(string relativeFilePath)
        {
            return File.ReadAllText($@"{_dataDirName}{Path.DirectorySeparatorChar}{relativeFilePath}");
        }

        private async Task CreateSchemas()
        {
            var scripts = new List<string>();
            _schemas.ForEach(schema => scripts.Add(GetFileContent($@"{schema}{Path.DirectorySeparatorChar}CreateSchema.sql")));

            await Execute(scripts);
        }

        private async Task SetUpDatabase()
        {
            var scripts = new List<string>();
            _schemas.ForEach(schema =>
            {
                scripts.Add(GetFileContent($@"{schema}{Path.DirectorySeparatorChar}InitSchema.sql"));
                scripts.Add(GetFileContent($@"{schema}{Path.DirectorySeparatorChar}InsertData.sql"));
            });

            await Execute(scripts);
        }

        private async Task Execute(List<string> scripts)
        {
            await using var connection = new SqlConnection(_msSqlContainer.GetConnectionString());
            connection.Open();
            await using var cmd = new SqlCommand();
            cmd.Connection = connection;

            scripts.ForEach(script =>
            {
                cmd.CommandText = script;
                using SqlDataReader reader = cmd.ExecuteReader();
            });
        }

        [TearDown]
        public async Task TearDown() 
        {
            await _msSqlContainer.DisposeAsync();
        }

        [Test]
        public async Task ConnectionStateReturnsOpen()
        {
            // Given
            using DbConnection connection = new SqlConnection(_msSqlContainer.GetConnectionString());

            // When
            await connection.OpenAsync();

            // Then
            Assert.That(connection.State, Is.EqualTo(ConnectionState.Open));
        }

        [Test]
        public async Task ExecScriptReturnsSuccessful()
        {
            // Given
            const string scriptContent = "SELECT 1;";

            // When
            var execResult = await _msSqlContainer.ExecScriptAsync(scriptContent);

            // When
            Assert.True(0L.Equals(execResult.ExitCode), execResult.Stderr);
        }
    }
}