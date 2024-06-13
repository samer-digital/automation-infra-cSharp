using Npgsql;
using System.Threading.Tasks;

/// <summary>
/// Manages PostgreSQL database connections.
/// </summary>
public class PgConnectionHolder : IDBConnectionHolder
{
    private NpgsqlConnection? _connection;

    /// <summary>
    /// Creates and opens a new PostgreSQL database connection asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result contains the created PostgreSQL connection.</returns>
    public async Task<object> CreateConnectionAsync()
    {
        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = ConfigProvider.PG_DB_HOST,
            Port = ConfigProvider.PG_DB_PORT,
            Database = ConfigProvider.PG_DB_NAME,
            Username = ConfigProvider.PG_DB_USER,
            Password = ConfigProvider.PG_DB_PASSWORD
        }.ConnectionString;

        _connection = new NpgsqlConnection(connectionString);
        await _connection.OpenAsync();
        return _connection;
    }

    /// <summary>
    /// Disposes the PostgreSQL database connection asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous dispose operation.</returns>
    public async Task DisposeAsync()
    {
        if (_connection != null)
        {
            await _connection.CloseAsync();
            _connection = null;
        }
    }
}
