using MongoDB.Driver;

/// <summary>
/// Provides MongoDB-specific logic for the User collection.
/// </summary>
public class MongoUserLogic : MongoDBLogicBase<MongoUser>
{
    public override DBType DbType => DBType.Mongo;

    /// <summary>
    /// Gets users by their first name.
    /// </summary>
    /// <param name="firstName">The first name to search for.</param>
    /// <returns>A list of users with the specified first name.</returns>
    public async Task<List<MongoUser>> GetByFirstNameAsync(string firstName)
    {
        var filter = Builders<MongoUser>.Filter.Eq(u => u.FirstName, firstName);
        return await FindAsync(filter);
    }
}
