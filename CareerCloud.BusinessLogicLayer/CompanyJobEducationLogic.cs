using CareerCloud.BusinessLogicLayer.Helpers;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
{
    public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository)
    {
    }

    public override void Add(CompanyJobEducationPoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    public override void Update(CompanyJobEducationPoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(CompanyJobEducationPoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.Major))
            {
                validationExceptions.Add(new ValidationException(
                  ExceptionCodes.CompanyJobEducation_Major,
                  $"{nameof(poco.Major)} Cannot be empty and must be at least 2 characters"));
            }
            else if (poco.Major.Length < 2)
            {
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyJobEducation_Major,
                                  $"{nameof(poco.Major)}: {poco.Major} must be at least 2 characters"));
            }


            if (poco.Importance < 0)
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyJobEducation_Importance,
                                  $"{nameof(poco.Importance)}: {poco.Importance} cannot be less than 0"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}