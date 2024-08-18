using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Company_Job_Educations")]

public class CompanyJobEducationPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }

    [Column("Importance")]
    public Int16 Importance { get; set; }

    //TODO: ForeignKey: CompanyJobPoco.Id
    [ForeignKey(nameof(Job))]
    public CompanyJobPoco CompanyJob { get; set; }

    [Column("Job")]
    public Guid Job { get; set; }

    [Column("Major")]
    public string Major { get; set; }

    [Column("Time_Stamp"), Timestamp]
    public byte[] TimeStamp { get; set; }
}