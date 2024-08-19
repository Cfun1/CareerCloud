using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Security_Logins_Log")]

public class SecurityLoginsLogPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    [ForeignKey(nameof(Login))]
    public SecurityLoginPoco SecurityLogin { get; set; }
    #endregion


    [Column("Is_Succesful")]
    public bool IsSuccesful { get; set; }


    [Column("Login")]
    public Guid Login { get; set; }


    [Column("Logon_Date", TypeName = $"{SqlTypes.DATETIME}")]
    public DateTime LogonDate { get; set; }


    [Column("Source_IP", TypeName = $"{SqlTypes.CHAR}(15)")]
    public string SourceIP { get; set; }
}