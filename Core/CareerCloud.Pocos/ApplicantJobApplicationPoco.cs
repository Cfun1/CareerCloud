using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CareerCloud.Pocos;

[Table("Applicant_Job_Applications")]

public class ApplicantJobApplicationPoco : IPoco, IRowVersion
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    [ForeignKey(nameof(Applicant))]
    public ApplicantProfilePoco ApplicantProfile { get; set; } = null!;


    [ForeignKey(nameof(Job))]
    public CompanyJobPoco CompanyJob { get; set; } = null!;
    #endregion


    [Column("Applicant")]
    public Guid Applicant { get; set; }


    [Column("Application_Date", TypeName = SqlTypes.DATETIME2)]
    public DateTime ApplicationDate { get; set; }


    [Column("Job")]
    public Guid Job { get; set; }


    [Column("Time_Stamp", TypeName = $"{SqlTypes.TIMESTAMP}")]
    [JsonIgnore, Timestamp]
    public byte[] TimeStamp { get; set; }
}