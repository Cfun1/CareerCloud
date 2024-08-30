using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Applicant_Skills")]

public class ApplicantSkillPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    [ForeignKey(nameof(Applicant))]
    public ApplicantProfilePoco ApplicantProfile { get; set; } = null!;
    #endregion


    [Column("Applicant")]
    public Guid Applicant { get; set; }


    [Column("Skill", TypeName = $"{SqlTypes.NVARCHAR}(100)")]
    public string Skill { get; set; }


    [Column("Skill_Level", TypeName = $"{SqlTypes.CHAR}(10)")]
    public string SkillLevel { get; set; }


    [Column("Start_Month", TypeName = SqlTypes.TINYINT)]
    public byte StartMonth { get; set; }


    [Column("End_Month", TypeName = SqlTypes.TINYINT)]
    public byte EndMonth { get; set; }


    [Column("Start_Year", TypeName = SqlTypes.INT)]
    public Int32 StartYear { get; set; }


    [Column("End_Year", TypeName = SqlTypes.INT)]
    public Int32 EndYear { get; set; }


    [Column("Time_Stamp", TypeName = SqlTypes.TIMESTAMP), Timestamp]
    public byte[] TimeStamp { get; set; }
}