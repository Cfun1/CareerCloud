using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CareerCloud.Pocos;

[Table("Security_Logins")]

public class SecurityLoginPoco : IPoco, IRowVersion
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    public virtual IList<ApplicantProfilePoco> ApplicantProfiles { get; set; } = null!;
    public virtual IList<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; } = null!;
    public virtual IList<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; } = null!;
    #endregion


    [Column("Agreement_Accepted_Date")]
    public DateTime? AgreementAccepted { get; set; }


    [Column("Created_Date", TypeName = $"{SqlTypes.DATETIME2}")]
    public DateTime Created { get; set; }


    [Column("Email_Address", TypeName = $"{SqlTypes.VARCHAR}(50)")]
    public string EmailAddress { get; set; }


    [Column("Force_Change_Password")]
    public bool ForceChangePassword { get; set; }


    [Column("Full_Name", TypeName = $"{SqlTypes.NVARCHAR}(100)")]
    public string? FullName { get; set; }


    [Column("Is_Inactive")]
    public bool IsInactive { get; set; }


    [Column("Is_Locked")]
    public bool IsLocked { get; set; }


    [Column("Login", TypeName = $"{SqlTypes.VARCHAR}(50)")]
    public string Login { get; set; }


    [Column("Password", TypeName = $"{SqlTypes.VARCHAR}(100)")]
    public string Password { get; set; }


    [Column("Password_Update_Date")]
    public DateTime? PasswordUpdate { get; set; }


    [Column("Phone_Number", TypeName = $"{SqlTypes.VARCHAR}(20)")]
    public string? PhoneNumber { get; set; }


    [Column("Prefferred_Language", TypeName = $"{SqlTypes.CHAR}(10)")]
    public string? PrefferredLanguage { get; set; }


    [Column("Time_Stamp", TypeName = $"{SqlTypes.TIMESTAMP}")]
    [JsonIgnore, Timestamp]
    public byte[] TimeStamp { get; set; }
}