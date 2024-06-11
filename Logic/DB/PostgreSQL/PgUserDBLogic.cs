using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PgUserDBLogic : PgLogicBase
{
    public override DBType DbType => DBType.PG;

    public async Task<List<PgUser>> GetByFirstNameAsync(string firstName)
    {
        var users = new List<PgUser>();
        var query = "SELECT * FROM users WHERE first_name = @firstName";
        
        using (var command = new NpgsqlCommand(query, _connection))
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
                    var user = new PgUser
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
