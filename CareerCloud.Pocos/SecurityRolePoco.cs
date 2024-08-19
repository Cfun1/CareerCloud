using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Security_Roles")]

public class SecurityRolePoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    public virtual IList<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; } = null!;
    #endregion


    [Column("Is_Inactive")]
    public bool IsInactive { get; set; }


    [Column("Role", TypeName = $"{SqlTypes.VARCHAR}(50)")]
    public string Role { get; set; }
}