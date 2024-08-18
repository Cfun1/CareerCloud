using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Applicant_Job_Applications")]

public class ApplicantJobApplicationPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }

    //TODO: ForeignKey: ApplicantProfilePoco.Id
    [ForeignKey("Applicant")]
    public ApplicantProfilePoco ApplicantProfile { get; set; }

    [Column("Applicant")]
    public Guid Applicant { get; set; }

    [Column("Application_Date")]
    public DateTime ApplicationDate { get; set; }

    //TODO: ForeignKey: CompanyJobPoco.Id
    [ForeignKey("Job")]
    public CompanyJobPoco CompanyJob { get; set; }

    [Column("Job")]
    public Guid Job { get; set; }

    [Column("Time_Stamp")]
    public byte[] TimeStamp { get; set; }
}