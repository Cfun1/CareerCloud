using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CareerCloud.Pocos;

[Table("Company_Jobs")]

public class CompanyJobPoco : IPoco, IRowVersion
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    [ForeignKey(nameof(Company))]
    public CompanyProfilePoco CompanyProfile { get; set; } = null!;


    public virtual IList<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; } = null!;
    public virtual IList<CompanyJobSkillPoco> CompanyJobSkills { get; set; } = null!;
    public virtual IList<CompanyJobEducationPoco> CompanyJobEducations { get; set; } = null!;
    public virtual IList<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; } = null!;
    #endregion

    [Column("Company")]
    public Guid Company { get; set; }


    [Column("Is_Company_Hidden")]
    public bool IsCompanyHidden { get; set; }


    [Column("Is_Inactive")]
    public bool IsInactive { get; set; }


    [Column("Profile_Created", TypeName = $"{SqlTypes.DATETIME2}")]
    public DateTime ProfileCreated { get; set; }


    [Column("Time_Stamp", TypeName = $"{SqlTypes.TIMESTAMP}")]
    [JsonIgnore, Timestamp]
    public byte[] TimeStamp { get; set; }
}