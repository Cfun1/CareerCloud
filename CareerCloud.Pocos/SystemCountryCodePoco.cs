using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("System_Country_Codes")]

public class SystemCountryCodePoco
{
    [Key, Column("Code", TypeName = $"{SqlTypes.CHAR}(10)")]
    public string Code { get; set; }


    [Column("Name", TypeName = $"{SqlTypes.NVARCHAR}(50)")]
    public string Name { get; set; }


    #region EF related
    public virtual CompanyLocationPoco CompanyLocation { get; set; }
    #endregion
}