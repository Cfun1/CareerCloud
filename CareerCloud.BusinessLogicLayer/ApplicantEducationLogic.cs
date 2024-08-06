using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
{
    public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
    {
    }

    public override void Add(ApplicantEducationPoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }

    protected override void Verify(ApplicantEducationPoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.Major) || poco.Major.Length < 3)
                validationExceptions.Add(
                    new ValidationException(107, "Cannot be empty or less than 3 characters"));

            if (poco.StartDate > DateTime.Now)
                validationExceptions.Add(
                    new ValidationException(108, $"{poco.StartDate} Cannot be greater than today's date {DateTime.Now}"));

            if (poco.CompletionDate < poco.StartDate)
                validationExceptions.Add(
                    new ValidationException(109, $"{poco.CompletionDate} CompletionDate cannot be earlier than StartDate {poco.StartDate}"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}