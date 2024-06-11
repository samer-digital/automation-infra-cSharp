using MongoDB.Driver;

public class MongoDBConnectionHolder : IDBConnectionHolder
{
    private IMongoDatabase? _database;

    public Task<object> CreateConnectionAsync()
    {
        var client = new MongoClient(new MongoClientSettings
        {
            Server = new MongoServerAddress(ConfigProvider.MONGO_DB_HOST, ConfigProvider.MONGO_DB_PORT),
            Credential = MongoCredential.CreateCredential(
                ConfigProvider.MONGO_DB_NAME,
                ConfigProvider.MONGO_DB_NAME,
                ConfigProvider.MONGO_DB_PASSWORD
            ),
            ApplicationName = "default"
        });

        _database = client.GetDatabase(ConfigProvider.MONGO_DB_NAME);
        return Task.FromResult<object>(_database);
    }

    public Task DisposeAsync()
    {
        // MongoDB C# driver handles connections automatically
        _database = null;
        return Task.CompletedTask;
    }
}