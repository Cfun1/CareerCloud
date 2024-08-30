using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Applicant_Profiles")]

public class ApplicantProfilePoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    [ForeignKey(nameof(Country))]
    public SystemCountryCodePoco? SystemCountryCode { get; set; }

    [ForeignKey(nameof(Login))]
    public SecurityLoginPoco SecurityLogin { get; set; } = null!;


    public virtual IList<ApplicantSkillPoco>? ApplicantSkills { get; set; }
    public virtual IList<ApplicantEducationPoco>? ApplicantEducations { get; set; }
    public virtual IList<ApplicantJobApplicationPoco>? ApplicantJobApplications { get; set; }
    public virtual IList<ApplicantResumePoco>? ApplicantResumes { get; set; }
    public virtual IList<ApplicantWorkHistoryPoco>? ApplicantWorkHistorys { get; set; }
    #endregion


    [Column("City_Town", TypeName = $"{SqlTypes.NVARCHAR}(100)")]
    public string? City { get; set; }


    [Column("Country_Code")]
    public string? Country { get; set; }


    [Column("Current_Rate")]
    public decimal? CurrentRate { get; set; }


    [Column("Current_Salary", TypeName = $"{SqlTypes.DECIMAL}(18, 0)")]
    public decimal? CurrentSalary { get; set; }


    [Column("Currency", TypeName = $"{SqlTypes.CHAR}(10)")]
    public string? Currency { get; set; }


    [Column("Login")]
    public Guid Login { get; set; }


    [Column("Zip_Postal_Code", TypeName = $"{SqlTypes.CHAR}(20)")]
    public string? PostalCode { get; set; }


    [Column("State_Province_Code", TypeName = $"{SqlTypes.CHAR}(10)")]
    public string? Province { get; set; }


    [Column("Street_Address", TypeName = $"{SqlTypes.NVARCHAR}(100)")]
    public string? Street { get; set; }


    [Column("Time_Stamp", TypeName = SqlTypes.TIMESTAMP), Timestamp]
    public byte[] TimeStamp { get; set; }
}