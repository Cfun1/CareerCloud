using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Company_Profiles")]

public class CompanyProfilePoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    public virtual IList<CompanyLocationPoco> CompanyLocations { get; set; }

    public virtual IList<CompanyDescriptionPoco> CompanyDescriptions { get; set; }

    public virtual IList<CompanyJobPoco> CompanyJobs { get; set; }
    #endregion


    [Column("Company_Logo")]
    public byte[]? CompanyLogo { get; set; }


    [Column("Company_Website", TypeName = $"{SqlTypes.VARCHAR}(100)")]
    public string? CompanyWebsite { get; set; }


    [Column("Contact_Name", TypeName = $"{SqlTypes.VARCHAR}(50)")]
    public string? ContactName { get; set; }


    [Column("Contact_Phone", TypeName = $"{SqlTypes.VARCHAR}(20)")]
    public string ContactPhone { get; set; }


    [Column("Registration_Date", TypeName = $"{SqlTypes.DATETIME2}")]
    public DateTime RegistrationDate { get; set; }


    [Column("Time_Stamp", TypeName = $"{SqlTypes.TIMESTAMP}"), Timestamp]
    public byte[]? TimeStamp { get; set; }
}