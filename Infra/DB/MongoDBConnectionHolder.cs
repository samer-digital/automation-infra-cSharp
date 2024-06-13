using MongoDB.Driver;
using System.Threading.Tasks;

/// <summary>
/// Manages MongoDB database connections.
/// </summary>
public class MongoDBConnectionHolder : IDBConnectionHolder
{
    private IMongoDatabase? _database;

    /// <summary>
    /// Creates and initializes a new MongoDB database connection asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation. The task result contains the created MongoDB database connection.</returns>
    public Task<object> CreateConnectionAsync()
    {
        var client = new MongoClient(new MongoClientSettings
        {
            Server = new MongoServerAddress(ConfigProvider.MONGO_DB_HOST, ConfigProvider.MONGO_DB_PORT),
            Credential = MongoCredential.CreateCredential(
                ConfigProvider.MONGO_DB_NAME,
                ConfigProvider.MONGO_DB_USER, // Changed to correct user name field
                ConfigProvider.MONGO_DB_PASSWORD
            ),
            ApplicationName = "default"
        });

        _database = client.GetDatabase(ConfigProvider.MONGO_DB_NAME);
        return Task.FromResult<object>(_database);
    }

    /// <summary>
    /// Disposes the MongoDB database connection.
    /// </summary>
    /// <returns>A completed task representing the dispose operation.</returns>
    public Task DisposeAsync()
    {
        // MongoDB C# driver handles connections automatically
        _database = null;
        return Task.CompletedTask;
    }
}
