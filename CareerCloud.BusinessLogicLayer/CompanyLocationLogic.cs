using CareerCloud.BusinessLogicLayer.Helpers;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
{
    public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
    {
    }

    public override void Add(CompanyLocationPoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    public override void Update(CompanyLocationPoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }
    protected override void Verify(CompanyLocationPoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.CountryCode))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyLocation_CountryCode,
                                  $"{nameof(poco.CountryCode)} cannot be empty"));

            if (string.IsNullOrEmpty(poco.Province))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyLocation_Province,
                                  $"{nameof(poco.Province)} cannot be empty"));

            if (string.IsNullOrEmpty(poco.Street))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyLocation_Street,
                                  $"{nameof(poco.Street)} cannot be empty"));

            if (string.IsNullOrEmpty(poco.City))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyLocation_City,
                                  $"{nameof(poco.City)} cannot be empty"));


            if (string.IsNullOrEmpty(poco.PostalCode))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyLocation_PostalCode,
                                  $"{nameof(poco.PostalCode)} cannot be empty"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}