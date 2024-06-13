/// <summary>
/// Represents the base interface for database logic classes.
/// </summary>
public interface IDBLogicBase
{
    /// <summary>
    /// Gets the type of the database.
    /// </summary>
    DBType DbType { get; }

    /// <summary>
    /// Sets the database connection.
    /// </summary>
    /// <param name="connection">The database connection object.</param>
    void SetConnection(object connection);
}
