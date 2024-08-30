using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos;

[Table("Applicant_Resumes")]

public class ApplicantResumePoco : IPoco
{
    [Key, Column("Id")]
    public Guid Id { get; set; }

    #region EF navigation
    [ForeignKey(nameof(Applicant))]
    public ApplicantProfilePoco ApplicantProfile { get; set; } = null!;
    #endregion


    [Column("Applicant")]
    public Guid Applicant { get; set; }


    [Column("Last_Updated", TypeName = $"{SqlTypes.DATETIME2}")]
    public DateTime? LastUpdated { get; set; }


    [Column("Resume")]
    public string Resume { get; set; }
}