using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class CompanyJobDescriptionLogic : BaseLogic<CompanyJobDescriptionPoco>
{
    public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repository) : base(repository)
    {
    }

    public override void Add(CompanyJobDescriptionPoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    public override void Update(CompanyJobDescriptionPoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(CompanyJobDescriptionPoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.JobName))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyJobDescription_JobName,
                                  $"{nameof(poco.JobName)} cannot be empty"));

            if (string.IsNullOrEmpty(poco.JobDescriptions))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyJobDescription_JobDescriptions,
                                  $"{nameof(poco.JobDescriptions)} cannot be empty"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}