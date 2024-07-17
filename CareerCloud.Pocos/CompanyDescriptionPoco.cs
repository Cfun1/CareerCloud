using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Company_Descriptions")]

public class CompanyDescriptionPoco : IPoco
{

    [Key, Column("Id")]
    public Guid Id { get; set; }

    //TODO: ForeignKey: CompanyProfilePoco.Id
    [Column("Company")]
    public Guid Company { get; set; }

    [Column("Company_Description")]
    public string CompanyDescription { get; set; }

    [Column("Company_Name")]
    public string CompanyName { get; set; }

    //TODO: ForeignKey: SystemLanguageCodePoco.LanguageID
    [Column("LanguageID")]
    public string LanguageId { get; set; }

    [Column("Time_Stamp")]
    public byte[] TimeStamp { get; set; }
}