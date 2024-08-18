using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Security_Logins_Roles")]

public class SecurityLoginsRolePoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    //TODO: ForeignKey: SecurityLoginPoco.Id
    [ForeignKey(nameof(Login))]
    public SecurityLoginPoco SecurityLogin { get; set; }


    [Column("Login")]
    public Guid Login { get; set; }


    //TODO: ForeignKey: SecurityRolePoco.Id
    [ForeignKey(nameof(Role))]
    public SecurityRolePoco SecurityRole { get; set; }


    [Column("Role")]
    public Guid Role { get; set; }


    [Column("Time_Stamp"), Timestamp]
    public byte[]? TimeStamp { get; set; }
}