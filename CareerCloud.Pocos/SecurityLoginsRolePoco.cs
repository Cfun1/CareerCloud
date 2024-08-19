using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Security_Logins_Roles")]

public class SecurityLoginsRolePoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    [ForeignKey(nameof(Login))]
    public SecurityLoginPoco SecurityLogin { get; set; }


    [ForeignKey(nameof(Role))]
    public SecurityRolePoco SecurityRole { get; set; }
    #endregion


    [Column("Login")]
    public Guid Login { get; set; }


    [Column("Role")]
    public Guid Role { get; set; }


    [Column("Time_Stamp", TypeName = $"{SqlTypes.TIMESTAMP}"), Timestamp]
    public byte[]? TimeStamp { get; set; }
}