using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CareerCloud.Pocos;

[Table("Company_Job_Educations")]

public class CompanyJobEducationPoco : IPoco, IRowVersion
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    [ForeignKey(nameof(Job))]
    public CompanyJobPoco CompanyJob { get; set; } = null!;
    #endregion


    [Column("Importance", TypeName = $"{SqlTypes.SMALLINT}")]
    public Int16 Importance { get; set; }


    [Column("Job")]
    public Guid Job { get; set; }


    [Column("Major", TypeName = $"{SqlTypes.NVARCHAR}(100)")]
    public string Major { get; set; }


    [Column("Time_Stamp", TypeName = $"{SqlTypes.TIMESTAMP}")]
    [JsonIgnore, Timestamp]
    public byte[] TimeStamp { get; set; }
}