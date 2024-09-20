using CareerCloud.Pocos;

namespace CareerCloud.DataTransfer;

public static class ApplicantEducationDtoExtensions
{
    public static ApplicantEducationPoco ToModel(this ApplicantEducationDto dto)
        => new ApplicantEducationPoco()
        {
            Id = dto.Id,
            Applicant = dto.Applicant,
            StartDate = dto.StartDate,
            CompletionDate = dto.CompletionDate,
            CertificateDiploma = dto.CertificateDiploma,
            CompletionPercent = dto.CompletionPercent,
            Major = dto.Major,
            TimeStamp = [0] //otherwise api controlelr validation failed
        };

    public static IEnumerable<ApplicantEducationPoco> ToModel(this IEnumerable<ApplicantEducationDto> dtos)
    {
        return dtos.Select(dto => dto.ToModel());
    }
}

public static class ApplicantEducationPocoExtensions
{
    public static ApplicantEducationDto ToDto(this ApplicantEducationPoco poco)
        => new ApplicantEducationDto(
            Id: poco.Id,
            Applicant: poco.Applicant,
            CertificateDiploma: poco.CertificateDiploma,
            StartDate: poco.StartDate,
            CompletionPercent: poco.CompletionPercent,
            Major: poco.Major,
            CompletionDate: poco.CompletionDate
            );

    public static IEnumerable<ApplicantEducationDto> ToDto(this IEnumerable<ApplicantEducationPoco> pocos)
    {
        return pocos.Select(poco => poco.ToDto());
    }
}
