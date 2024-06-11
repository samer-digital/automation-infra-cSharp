using MySql.Data.MySqlClient;

public class MySQLConnectionHolder : IDBConnectionHolder
{
    private MySqlConnection? _connection;


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

    public async Task DisposeAsync()
    {
        if (_connection != null)
        {
            await _connection.CloseAsync();
            _connection = null;
        }
    }
}