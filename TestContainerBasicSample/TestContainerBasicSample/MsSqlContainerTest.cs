using System.Data.Common;
using System.Data;
using Testcontainers.MsSql;
using Microsoft.Data.SqlClient;

namespace TestContainerBasicSample;

public sealed class MsSqlContainerTest : IAsyncLifetime
{
    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    public Task InitializeAsync()
    {
        return _msSqlContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return _msSqlContainer.DisposeAsync().AsTask();
    }

    [Fact]
    public void ConnectionStateReturnsOpen()
    {
        // Given
        using DbConnection connection = new SqlConnection(_msSqlContainer.GetConnectionString());

        // When
        connection.Open();

        // Then
        Assert.Equal(ConnectionState.Open, connection.State);
    }

    [Fact]
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