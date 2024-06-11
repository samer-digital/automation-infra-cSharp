using Microsoft.Extensions.Configuration;

/// <summary>
/// Manages database connections and logic for multiple database types.
/// </summary>
public class DBContext
{
    private readonly IDictionary<DBType, IDBConnectionHolder> _connectionHolders;

    public DBContext()
    {
        _connectionHolders = new Dictionary<DBType, IDBConnectionHolder>();
    }

    /// <summary>
    /// Gets the connection holder for the specified database type.
    /// </summary>
    /// <param name="dbType">The type of the database.</param>
    /// <returns>The connection holder for the specified database type.</returns>
    /// <exception cref="ArgumentException">Thrown when an unsupported database type is provided.</exception>
    private IDBConnectionHolder GetConnectionHolderForDb(DBType dbType)
    {
        if (!_connectionHolders.ContainsKey(dbType))
        {
            switch (dbType)
            {
                case DBType.PG:
                    _connectionHolders[dbType] = new PgConnectionHolder();
                    break;
                case DBType.MySQL:
                    _connectionHolders[dbType] = new MySQLConnectionHolder();
                    break;
                case DBType.Mongo:
                    _connectionHolders[dbType] = new MongoDBConnectionHolder();
                    break;
                default:
                    throw new ArgumentException("Unsupported DB type");
            }
        }
        return _connectionHolders[dbType];
    }

    /// <summary>
    /// Gets the database logic instance asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the database logic.</typeparam>
    /// <param name="dbLogicFactory">The factory method to create the database logic instance.</param>
    /// <returns>The database logic instance.</returns>
    public async Task<T> GetDBLogicAsync<T>(Func<T> dbLogicFactory) where T : IDBLogicBase
    {
        var dbLogicInstance = dbLogicFactory();
        var connectionHolder = GetConnectionHolderForDb(dbLogicInstance.DbType);
        var connection = await connectionHolder.CreateConnectionAsync();
        dbLogicInstance.SetConnection(connection);
        return dbLogicInstance;
    }

    /// <summary>
    /// Disposes the database context asynchronously.
    /// </summary>
    public async Task DisposeAsync()
    {
        foreach (var connectionHolder in _connectionHolders.Values)
        {
            await connectionHolder.DisposeAsync();
        }
    }
}