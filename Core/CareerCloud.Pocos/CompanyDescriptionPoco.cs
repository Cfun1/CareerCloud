using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CareerCloud.Pocos;

[Table("Company_Descriptions")]

public class CompanyDescriptionPoco : IPoco, IRowVersion
{

    [Key, Column("Id")]
    public Guid Id { get; set; }

    #region EF navigation
    [ForeignKey(nameof(Company))]
    public CompanyProfilePoco CompanyProfile { get; set; } = null!;


    [ForeignKey(nameof(LanguageId))]
    public SystemLanguageCodePoco SystemLanguageCode { get; set; } = null!;
    #endregion


    [Column("Company")]
    public Guid Company { get; set; }


    [Column("Company_Description", TypeName = $"{SqlTypes.NVARCHAR}(1000)")]
    public string CompanyDescription { get; set; }


    [Column("Company_Name", TypeName = $"{SqlTypes.NVARCHAR}(50)")]
    public string CompanyName { get; set; }


    [Column("LanguageID", TypeName = $"{SqlTypes.CHAR}(10)")]
    public string LanguageId { get; set; }


    [Column("Time_Stamp", TypeName = $"{SqlTypes.TIMESTAMP}")]
    [JsonIgnore, Timestamp]
    public byte[] TimeStamp { get; set; }
}