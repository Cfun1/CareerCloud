using CareerCloud.Pocos;

namespace CareerCloud.DataTransfer;

public class ApplicantEducationDto : IPoco
{
    public Guid Id { get; set; }

    public ApplicantProfilePoco? ApplicantProfile { get; set; }

    public Guid Applicant { get; set; }

    public string? CertificateDiploma { get; set; }

    public DateTime? CompletionDate { get; set; }

    public byte? CompletionPercent { get; set; }

    public string Major { get; set; }

    public DateTime? StartDate { get; set; }
}