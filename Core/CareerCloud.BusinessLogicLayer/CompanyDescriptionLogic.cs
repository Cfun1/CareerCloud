using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class CompanyDescriptionLogic : BaseLogic<CompanyDescriptionPoco>
{
    public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
    {
    }

    public override void Add(CompanyDescriptionPoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    public override void Update(CompanyDescriptionPoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(CompanyDescriptionPoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.CompanyDescription))
            {
                validationExceptions.Add(new ValidationException(
                  ExceptionCodes.CompanyDescription_CompanyDescription,
                  $"{nameof(poco.CompanyDescription)} cannot be empty and must be greater than 2 characters"));
            }

            else if (poco.CompanyDescription.Length <= 2)
            {
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyDescription_CompanyDescription,
                                  $"{nameof(poco.CompanyDescription)}: {poco.CompanyDescription} must be greater than 2 characters"));
            }

            if (string.IsNullOrEmpty(poco.CompanyName))
            {
                validationExceptions.Add(new ValidationException(
                  ExceptionCodes.CompanyDescription_CompanyName,
                  $"{nameof(poco.CompanyName)} cannot be empty and must be greater than 2 characters"));
            }

            else if (poco.CompanyName.Length <= 2)
            {
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyDescription_CompanyName,
                                  $"{nameof(poco.CompanyName)}: {poco.CompanyName} must be greater than 2 characters"));
            }
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}