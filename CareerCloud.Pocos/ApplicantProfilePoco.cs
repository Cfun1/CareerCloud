using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Applicant_Profiles")]

public class ApplicantProfilePoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    [Column("City_Town")]
    public string? City { get; set; }

    //TODO: ForeignKey: SystemCountryCodePoco.Code
    [ForeignKey(nameof(Country))]
    public SystemCountryCodePoco SystemCountryCode { get; set; }


    [Column("Country_Code")]
    public string? Country { get; set; }


    [Column("Currency")]
    public string? Currency { get; set; }


    [Column("Current_Rate")]
    public decimal? CurrentRate { get; set; }


    [Column("Current_Salary")]
    public decimal? CurrentSalary { get; set; }


    //TODO: ForeignKey: SecurityLogin.Id
    [ForeignKey(nameof(Login))]
    public SecurityLoginPoco SecurityLogin { get; set; }


    [Column("Login")]
    public Guid Login { get; set; }


    [Column("State_Province_Code")]
    public string? Province { get; set; }


    [Column("Street_Address")]
    public string? Street { get; set; }


    [Column("Time_Stamp"), Timestamp]
    public byte[] TimeStamp { get; set; }


    [Column("Zip_Postal_Code")]
    public string? PostalCode { get; set; }

    #region EF related
    public virtual IList<ApplicantSkillPoco> ApplicantSkills { get; set; }
    public virtual IList<ApplicantEducationPoco> ApplicantEducations { get; set; }
    public virtual IList<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
    public virtual IList<ApplicantResumePoco> ApplicantResumes { get; set; }
    public virtual IList<ApplicantWorkHistoryPoco> ApplicantWorkHistorys { get; set; }
    #endregion
}