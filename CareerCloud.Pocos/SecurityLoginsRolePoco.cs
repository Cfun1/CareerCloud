using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Security_Logins_Roles")]

public class SecurityLoginsRolePoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }

    //TODO: ForeignKey: SecurityLoginPoco.Id
    [ForeignKey("Login")]
    public SecurityLoginPoco SecurityLogin { get; set; }


    [Column("Login")]
    public Guid Login { get; set; }

    //TODO: ForeignKey: SecurityRolePoco.Id
    [ForeignKey("Role")]
    public SecurityRolePoco SecurityRole { get; set; }

    [Column("Role")]
    public Guid Role { get; set; }

    [Column("Time_Stamp")]
    public byte[]? TimeStamp { get; set; }
}