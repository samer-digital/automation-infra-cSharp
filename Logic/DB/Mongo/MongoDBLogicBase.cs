using MongoDB.Driver;

public abstract class MongoDBLogicBase<T> : IDBLogicBase where T : class
{
    protected IMongoDatabase? _database;
    protected IMongoCollection<T>? _collection;

    public abstract DBType DbType { get; }

    public void SetConnection(object connection)
    {
        _database = (IMongoDatabase)connection;
        _collection = _database.GetCollection<T>(typeof(T).Name.ToLower() + "s");
    }

    public async Task<List<T>> FindAsync(FilterDefinition<T> filter)
    {
        if (_collection == null)
            throw new InvalidOperationException("Collection is not initialized.");

        return await _collection.Find(filter).ToListAsync();
    }

    public async Task<T> InsertAsync(T document)
    {
        if (_collection == null)
            throw new InvalidOperationException("Collection is not initialized.");

        await _collection.InsertOneAsync(document);
        return document;
    }
}
