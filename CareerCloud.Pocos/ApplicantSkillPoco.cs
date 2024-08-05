using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Applicant_Skills")]

public class ApplicantSkillPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }

    //TODO: ForeignKey: ApplicantProfilePoco.Id
    [Column("Applicant")]
    public Guid Applicant { get; set; }

    [Column("End_Month")]
    public byte EndMonth { get; set; }

    [Column("End_Year")]
    public Int32 EndYear { get; set; }


    [Column("Skill")]
    public string Skill { get; set; }

    [Column("Skill_Level")]
    public string SkillLevel { get; set; }

    [Column("Start_Month")]
    public byte StartMonth { get; set; }

    [Column("Start_Year")]
    public Int32 StartYear { get; set; }

    [Column("Time_Stamp")]
    public byte[] TimeStamp { get; set; }
}