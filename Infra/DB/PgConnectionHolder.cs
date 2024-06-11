using Npgsql;

public class PgConnectionHolder : IDBConnectionHolder
{
    private NpgsqlConnection? _connection;

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

    public async Task DisposeAsync()
    {
        if (_connection != null)
        {
            await _connection.CloseAsync();
            _connection = null;
        }
    }
}