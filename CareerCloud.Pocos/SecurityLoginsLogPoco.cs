using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Security_Logins_Log")]

public class SecurityLoginsLogPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    [Column("Is_Succesful")]
    public bool IsSuccesful { get; set; }


    //TODO: ForeignKey: SecurityLoginPoco.Id
    [ForeignKey(nameof(Login))]
    public SecurityLoginPoco SecurityLogin { get; set; }


    [Column("Login")]
    public Guid Login { get; set; }


    [Column("Logon_Date")]
    public DateTime LogonDate { get; set; }


    [Column("Source_IP")]
    public string SourceIP { get; set; }
}