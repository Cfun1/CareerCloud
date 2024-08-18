using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Company_Job_Skills")]

public class CompanyJobSkillPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }

    [Column("Importance")]
    public Int32 Importance { get; set; }

    //TODO: ForeignKey: CompanyJobPoco.Id

    [ForeignKey(nameof(Job))]
    public CompanyJobPoco CompanyJob { get; set; }

    [Column("Job")]
    public Guid Job { get; set; }

    [Column("Skill")]
    public string Skill { get; set; }

    [Column("Skill_Level")]
    public string SkillLevel { get; set; }

    [Column("Time_Stamp"), Timestamp]
    public byte[] TimeStamp { get; set; }
}