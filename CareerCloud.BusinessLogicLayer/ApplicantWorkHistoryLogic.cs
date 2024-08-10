using CareerCloud.BusinessLogicLayer.Helpers;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
{
    public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository)
    {
    }

    public override void Add(ApplicantWorkHistoryPoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    public override void Update(ApplicantWorkHistoryPoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.CompanyName))
            {
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.ApplicantWorkHistory_CompanyName,
                                  $"{nameof(poco.CompanyName)} Cannot be empty and must be greater than 2 characters"));
            }
            else if (poco.CompanyName.Length <= 2)
            {
                validationExceptions.Add(new ValidationException(
                         ExceptionCodes.ApplicantWorkHistory_CompanyName,
                         $"{nameof(poco.CompanyName)}: {poco.CompanyName} Must be greater than 2 characters"));
            }
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}