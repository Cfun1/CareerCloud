using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

/// <summary>
/// EXTRA: fluent API: schema db level: modelBuilder.HasDefaultSchema("dbo");
/// schema per table: ToTable("Applicant_Educations", schema: "dbo");
/// </summary>

[Table("Applicant_Educations")]
public class ApplicantEducationPoco : IPoco
{
    //fluent API:   .HasKey(c => c.Id);

    [Key, Column("Id")]
    public Guid Id { get; set; }

    //TODO: ForeignKey: ApplicantProfilePoco.Id
    //at api layer possibly need JsonIgnore attribute to avoid overflow exception caused by infinite relationship loop when serializing json
    //with EF, the property would be defined as virtual to enable lazy loading by EF
    [ForeignKey(nameof(Applicant))]
    public ApplicantProfilePoco ApplicantProfile { get; set; }

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