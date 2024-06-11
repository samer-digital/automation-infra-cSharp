using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class MongoUser
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    [BsonElement("firstName")]
    public string? FirstName { get; set; }

    [BsonElement("lastName")]
    public string? LastName { get; set; }

    [BsonElement("email")]
    public string? Email { get; set; }

    [BsonElement("passwordHash")]
    public string? PasswordHash { get; set; }
}
