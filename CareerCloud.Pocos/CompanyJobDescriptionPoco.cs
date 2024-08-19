using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Company_Jobs_Descriptions")]

public class CompanyJobDescriptionPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    [ForeignKey(nameof(Job))]
    public CompanyJobPoco CompanyJob { get; set; }
    #endregion

    [Column("Job")]
    public Guid Job { get; set; }


    [Column("Job_Descriptions", TypeName = $"{SqlTypes.NVARCHAR}(1000)")]
    public string? JobDescriptions { get; set; }


    [Column("Job_Name", TypeName = $"{SqlTypes.NVARCHAR}(100)")]
    public string? JobName { get; set; }


    [Column("Time_Stamp", TypeName = $"{SqlTypes.TIMESTAMP})"), Timestamp]
    public byte[]? TimeStamp { get; set; }
}