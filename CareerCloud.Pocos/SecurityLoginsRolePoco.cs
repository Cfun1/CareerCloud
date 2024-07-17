using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Security_Logins_Roles")]

public class SecurityLoginsRolePoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }

    //TODO: ForeignKey: SecurityLoginPoco.Id
    [Column("Login")]
    public Guid Login { get; set; }

    //TODO: ForeignKey: SecurityRolePoco.Id
    [Column("Role")]
    public Guid Role { get; set; }

    [Column("Time_Stamp")]
    public byte[]? TimeStamp { get; set; }
}