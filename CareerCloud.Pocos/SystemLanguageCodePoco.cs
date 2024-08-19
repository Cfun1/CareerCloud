using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("System_Language_Codes")]

public class SystemLanguageCodePoco
{
    [Key, Column("LanguageID", TypeName = $"{SqlTypes.CHAR}(10)")]
    public string LanguageID { get; set; }


    [Column("Name", TypeName = $"{SqlTypes.NVARCHAR}(50)")]
    public string Name { get; set; }


    [Column("Native_Name", TypeName = $"{SqlTypes.NVARCHAR}(50)")]
    public string NativeName { get; set; }


    #region EF related
    public CompanyDescriptionPoco CompanyDescription { get; set; }
    #endregion
}