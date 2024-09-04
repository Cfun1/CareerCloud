using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CareerCloud.Pocos;

[Table("Security_Logins_Roles")]

public class SecurityLoginsRolePoco : IPoco, IRowVersion
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    [ForeignKey(nameof(Login))]
    public SecurityLoginPoco SecurityLogin { get; set; } = null!;


    [ForeignKey(nameof(Role))]
    public SecurityRolePoco SecurityRole { get; set; } = null!;
    #endregion


    [Column("Login")]
    public Guid Login { get; set; }


    [Column("Role")]
    public Guid Role { get; set; }


    [Column("Time_Stamp", TypeName = $"{SqlTypes.TIMESTAMP}")]
    [JsonIgnore, Timestamp]
    public byte[]? TimeStamp { get; set; }
}