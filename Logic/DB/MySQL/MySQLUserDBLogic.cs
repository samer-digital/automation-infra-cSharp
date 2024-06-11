using MySql.Data.MySqlClient;

public class MySQLUserDBLogic : MySQLLogicBase
{
    public override DBType DbType => DBType.MySQL;

    public async Task<List<MySQLUser>> GetByFirstNameAsync(string firstName)
    {
        var users = new List<MySQLUser>();
        var query = "SELECT * FROM users WHERE first_name = @firstName";
        
        using (var command = new MySqlCommand(query, _connection))
        {
            command.Parameters.AddWithValue("@firstName", firstName);
            using (var reader = await command.ExecuteReaderAsync())
            {
                var idOrdinal = reader.GetOrdinal("id");
                var firstNameOrdinal = reader.GetOrdinal("first_name");
                var lastNameOrdinal = reader.GetOrdinal("last_name");
                var emailOrdinal = reader.GetOrdinal("email");
                var passwordHashOrdinal = reader.GetOrdinal("password_hash");

                while (await reader.ReadAsync())
                {
                    var user = new MySQLUser
                    {
                        Id = reader.GetInt32(idOrdinal),
                        FirstName = reader.GetString(firstNameOrdinal),
                        LastName = reader.GetString(lastNameOrdinal),
                        Email = reader.GetString(emailOrdinal),
                        PasswordHash = reader.GetString(passwordHashOrdinal)
                    };
                    users.Add(user);
                }
            }
        }

        return users;
    }
}
