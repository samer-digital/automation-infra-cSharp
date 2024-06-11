using MySql.Data.MySqlClient;

public abstract class MySQLLogicBase : IDBLogicBase
{
    protected MySqlConnection? _connection;

    public abstract DBType DbType { get; }

    public void SetConnection(object connection)
    {
        if (_connection == null)
            throw new InvalidOperationException("Connection is not initialized.");

        _connection = (MySqlConnection)connection;
    }
}
