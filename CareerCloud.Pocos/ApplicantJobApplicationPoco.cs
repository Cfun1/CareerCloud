using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Applicant_Job_Applications")]

public class ApplicantJobApplicationPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }

    //TODO: ForeignKey: ApplicantProfilePoco.Id
    [ForeignKey(nameof(Applicant))]
    public ApplicantProfilePoco ApplicantProfile { get; set; }


    [Column("Applicant")]
    public Guid Applicant { get; set; }


    [Column("Application_Date", TypeName = SqlTypes.DATETIME2)]
    public DateTime ApplicationDate { get; set; }


    //TODO: ForeignKey: CompanyJobPoco.Id
    [ForeignKey(nameof(Job))]
    public CompanyJobPoco CompanyJob { get; set; }


    [Column("Job")]
    public Guid Job { get; set; }


    [Column("Time_Stamp", TypeName = SqlTypes.TIMESTAMP), Timestamp]
    public byte[] TimeStamp { get; set; }
}