using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("System_Country_Codes")]

public class SystemCountryCodePoco
{
    [Key, Column("Code")]
    public string Code { get; set; }

    [Column("Name")]
    public string Name { get; set; }

    public virtual CompanyLocationPoco CompanyLocation { get; set; }

}