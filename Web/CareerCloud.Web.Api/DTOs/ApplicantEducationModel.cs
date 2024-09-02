using CareerCloud.Pocos;

namespace CareerCloud.WebApp.API;


public class ApplicantEducationModel : IPoco
{
    public Guid Id { get; set; }

    //public ApplicantProfilePoco ApplicantProfile { get; set; } = null!;

    public Guid Applicant { get; set; }

    public string? CertificateDiploma { get; set; }

    public DateTime? CompletionDate { get; set; }

    public int? CompletionPercent { get; set; }

    public string Major { get; set; }

    public DateTime? StartDate { get; set; }
}