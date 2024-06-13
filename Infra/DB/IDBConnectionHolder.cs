/// <summary>
/// Represents a holder for database connections.
/// </summary>
public interface IDBConnectionHolder
{
    /// <summary>
    /// Creates a new database connection asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the database connection object.</returns>
    Task<object> CreateConnectionAsync();

    /// <summary>
    /// Disposes of the database connection asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DisposeAsync();
}
