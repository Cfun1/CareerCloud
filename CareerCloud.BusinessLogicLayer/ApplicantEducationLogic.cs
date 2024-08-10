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

    public override void Update(ApplicantEducationPoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(ApplicantEducationPoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.Major))
            {
                validationExceptions.Add(
                    new ValidationException(ExceptionCodes.ApplicantEducation_Major
                    , $"{nameof(poco.Major)} Cannot be empty or less than 3 characters"));
            }
            else if (poco.Major.Length < 3)
            {
                validationExceptions.Add(
                    new ValidationException(ExceptionCodes.ApplicantEducation_Major
                    , $"{nameof(poco.Major)}: {poco.Major}  Cannot be empty or less than 3 characters"));
            }

            if (poco.StartDate > DateTime.Now)
                validationExceptions.Add(
                    new ValidationException(ExceptionCodes.ApplicantEducation_StartDate,
                    $"{poco.StartDate} Cannot be greater than today's date {DateTime.Now}"));

            if (poco.CompletionDate < poco.StartDate)
                validationExceptions.Add(
                    new ValidationException(ExceptionCodes.ApplicantEducation_CompletionDate, $"{poco.CompletionDate} CompletionDate cannot be earlier than StartDate {poco.StartDate}"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}