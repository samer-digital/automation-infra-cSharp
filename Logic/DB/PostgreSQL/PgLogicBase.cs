using Npgsql;

public abstract class PgLogicBase : IDBLogicBase
{
    protected NpgsqlConnection? _connection;

    public abstract DBType DbType { get; }

    public void SetConnection(object connection)
    {
        if (_connection == null)
            throw new InvalidOperationException("Connection is not initialized.");
        _connection = (NpgsqlConnection)connection;
    }
}
