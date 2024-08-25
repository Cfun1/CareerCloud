using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;


[Table("Applicant_Educations")]

public class ApplicantEducationPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    //api layer might require to add JsonIgnore attribute to avoid overflow exception caused by infinite relationship loop when serializing json
    [ForeignKey(nameof(Applicant))]
    public ApplicantProfilePoco ApplicantProfile { get; set; } = null!;
    #endregion


    [Column("Applicant")]
    public Guid Applicant { get; set; }


    [Column("Certificate_Diploma", TypeName = $"{SqlTypes.NVARCHAR}(100)")]
    public string? CertificateDiploma { get; set; }


    [Column("Completion_Date", TypeName = SqlTypes.DATE)]
    public DateTime? CompletionDate { get; set; }


    [Column("Completion_Percent", TypeName = SqlTypes.TINYINT)]
    public byte? CompletionPercent { get; set; }


    [Column("Major", TypeName = $"{SqlTypes.NVARCHAR}(100)")]
    public string Major { get; set; }


    [Column("Start_Date", TypeName = SqlTypes.DATE)]
    public DateTime? StartDate { get; set; }


    [Column("Time_Stamp", TypeName = SqlTypes.TIMESTAMP), Timestamp]
    public byte[] TimeStamp { get; set; }
}