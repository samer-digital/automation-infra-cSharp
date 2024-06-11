public interface IDBLogicBase
{
    DBType DbType { get; }
    void SetConnection(object connection);
}