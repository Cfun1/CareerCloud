using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Applicant_Educations")]

public class ApplicantEducationPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }

    //TODO: ForeignKey: ApplicantProfilePoco.Id
    //at api layer possibly need JsonIgnore attribute to avoid overflow exception caused by infinite relationship loop when serializing json
    //with EF, the property would be defined as virtual to enable lazy loading by EF
    [Column("Applicant")]
    public Guid Applicant { get; set; }

    [Column("Certificate_Diploma")]
    public string? CertificateDiploma { get; set; }

    [Column("Completion_Date")]
    public DateTime? CompletionDate { get; set; }

    [Column("Completion_Percent")]
    public byte? CompletionPercent { get; set; }

    [Column("Major")]
    public string Major { get; set; }

    [Column("Start_Date")]
    public DateTime? StartDate { get; set; }

    [Column("Time_Stamp")]
    public byte[] TimeStamp { get; set; }
}