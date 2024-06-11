
public interface IDBConnectionHolder
{
    Task<object> CreateConnectionAsync();
    Task DisposeAsync();
}