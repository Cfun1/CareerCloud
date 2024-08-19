using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Company_Job_Skills")]

public class CompanyJobSkillPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    [Column("Importance", TypeName = $"{SqlTypes.INT}")]
    public Int32 Importance { get; set; }


    //TODO: ForeignKey: CompanyJobPoco.Id
    [ForeignKey(nameof(Job))]
    public CompanyJobPoco CompanyJob { get; set; }


    [Column("Job")]
    public Guid Job { get; set; }


    [Column("Skill", TypeName = $"{SqlTypes.NVARCHAR}(100)")]
    public string Skill { get; set; }


    [Column("Skill_Level", TypeName = $"{SqlTypes.VARCHAR}(10)")]
    public string SkillLevel { get; set; }


    [Column("Time_Stamp", TypeName = $"{SqlTypes.TIMESTAMP}"), Timestamp]
    public byte[] TimeStamp { get; set; }
}