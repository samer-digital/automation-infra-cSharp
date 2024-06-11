using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public class PgUser
{
    [Key]
    [Column("id")]
    public required int Id { get; set; }

    [Column("first_name")]
    [MaxLength(255)]
    public required string FirstName { get; set; }

    [Column("last_name")]
    [MaxLength(255)]
    public string? LastName { get; set; }

    [Column("email")]
    [MaxLength(255)]
    public string? Email { get; set; }

    [Column("password_hash")]
    [MaxLength(255)]
    public string? PasswordHash { get; set; }
}
