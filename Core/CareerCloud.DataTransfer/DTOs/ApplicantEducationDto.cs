namespace CareerCloud.DataTransfer;

public record ApplicantEducationDto(
    Guid Id,
    //ApplicantProfilePoco? ApplicantProfile,
    Guid Applicant,
    string? CertificateDiploma,
    DateTime? CompletionDate,
    byte? CompletionPercent,
    string Major,
    DateTime? StartDate
) : IDto;
