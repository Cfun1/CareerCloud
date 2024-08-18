using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Company_Jobs")]

public class CompanyJobPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }

    //TODO: ForeignKey: CompanyProfilePoco.Id
    [ForeignKey(nameof(Company))]
    public CompanyProfilePoco CompanyProfile { get; set; }


    [Column("Company")]
    public Guid Company { get; set; }

    [Column("Is_Company_Hidden")]
    public bool IsCompanyHidden { get; set; }

    [Column("Is_Inactive")]
    public bool IsInactive { get; set; }

    [Column("Profile_Created")]
    public DateTime ProfileCreated { get; set; }

    [Column("Time_Stamp"), Timestamp]
    public byte[]? TimeStamp { get; set; }

    public virtual IList<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
    public virtual IList<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
    public virtual IList<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
}