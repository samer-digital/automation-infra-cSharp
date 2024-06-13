using MySql.Data.MySqlClient;

/// <summary>
/// Manages MySQL database connections.
/// </summary>
public class MySQLConnectionHolder : IDBConnectionHolder
{
    private MySqlConnection? _connection;

    /// <summary>
    /// Creates and opens a new MySQL database connection asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result contains the created MySQL connection.</returns>
    public async Task<object> CreateConnectionAsync()
    {
        var connectionString = new MySqlConnectionStringBuilder
        {
            Server = ConfigProvider.MYSQL_DB_HOST,
            Port = uint.Parse(ConfigProvider.MYSQL_DB_PORT),
            Database = ConfigProvider.MYSQL_DB_NAME,
            UserID = ConfigProvider.MYSQL_DB_USER,
            Password = ConfigProvider.MYSQL_DB_PASSWORD
        }.ConnectionString;

        _connection = new MySqlConnection(connectionString);
        await _connection.OpenAsync();
        return _connection;
    }

    /// <summary>
    /// Disposes the MySQL database connection asynchronously.
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
