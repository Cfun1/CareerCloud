using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Company_Locations")]

public class CompanyLocationPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    [ForeignKey(nameof(Company))]
    public virtual CompanyProfilePoco CompanyProfile { get; set; }


    [ForeignKey(nameof(CountryCode))]
    public virtual SystemCountryCodePoco SystemCountryCode { get; set; }
    #endregion


    [Column("City_Town", TypeName = $"{SqlTypes.NVARCHAR}(100)")]
    public string? City { get; set; }

    [Column("Company")]
    public Guid Company { get; set; }


    [Column("Country_Code", TypeName = $"{SqlTypes.CHAR}(10)")]
    public string CountryCode { get; set; }


    [Column("State_Province_Code", TypeName = $"{SqlTypes.CHAR}(10)")]
    public string? Province { get; set; }


    [Column("Street_Address", TypeName = $"{SqlTypes.NVARCHAR}(100)")]
    public string? Street { get; set; }


    [Column("Time_Stamp", TypeName = $"{SqlTypes.TIMESTAMP}"), Timestamp]
    public byte[] TimeStamp { get; set; }


    [Column("Zip_Postal_Code", TypeName = $"{SqlTypes.CHAR}(20)")]
    public string? PostalCode { get; set; }
}