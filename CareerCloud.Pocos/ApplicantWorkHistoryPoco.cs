using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Applicant_Work_History")]

public class ApplicantWorkHistoryPoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }


    #region EF navigation
    [ForeignKey(nameof(Applicant))]
    public ApplicantProfilePoco ApplicantProfile { get; set; } = null!;

    [ForeignKey(nameof(CountryCode))]
    public SystemCountryCodePoco SystemCountryCode { get; set; } = null!;
    #endregion


    [Column("Applicant")]
    public Guid Applicant { get; set; }


    [Column("Company_Name", TypeName = $"{SqlTypes.NVARCHAR}(50)")]
    public string CompanyName { get; set; }


    [Column("Country_Code", TypeName = $"{SqlTypes.CHAR}(10)")]
    public string CountryCode { get; set; }


    [Column("Start_Month", TypeName = SqlTypes.SMALLINT)]
    public Int16 StartMonth { get; set; }


    [Column("End_Month", TypeName = SqlTypes.SMALLINT)]
    public Int16 EndMonth { get; set; }


    [Column("Start_Year", TypeName = SqlTypes.INT)]
    public Int32 StartYear { get; set; }


    [Column("End_Year", TypeName = SqlTypes.INT)]
    public Int32 EndYear { get; set; }


    [Column("Job_Description", TypeName = $"{SqlTypes.NVARCHAR}(500)")]
    public string JobDescription { get; set; }


    [Column("Job_Title", TypeName = $"{SqlTypes.NVARCHAR}(50)")]
    public string JobTitle { get; set; }


    [Column("Location", TypeName = $"{SqlTypes.NVARCHAR}(50)")]
    public string Location { get; set; }


    [Column("Time_Stamp"), Timestamp]
    public byte[] TimeStamp { get; set; }
}