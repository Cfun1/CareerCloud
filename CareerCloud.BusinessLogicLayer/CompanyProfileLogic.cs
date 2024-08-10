using CareerCloud.BusinessLogicLayer.Helpers;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
{
    public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
    {
    }

    public override void Add(CompanyProfilePoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }

    public override void Update(CompanyProfilePoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(CompanyProfilePoco[] pocos)
    {
        var allowedTLDs = new string[] { ".com", ".ca", ".biz" };

        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {

            if (string.IsNullOrEmpty(poco.CompanyWebsite))
            {
                validationExceptions.Add(new ValidationException(
                   ExceptionCodes.CompanyProfile_CompanyWebsite,
                   $"{nameof(poco.CompanyWebsite)}:  Valid websites cannot be empty and must end with the following extensions – \".ca\", \".com\", \".biz\""));
            }

            else if (!allowedTLDs.Any(tld => poco.CompanyWebsite.EndsWith(tld)))
            {
                validationExceptions.Add(new ValidationException(
                                 ExceptionCodes.CompanyProfile_CompanyWebsite,
                                 $"{nameof(poco.CompanyWebsite)}: {poco.CompanyWebsite} Valid websites must                end with the following extensions – \".ca\", \".com\", \".biz\""));
            }

            //todo: validate phone with regex,  insted check already implemented in SecurityLoginLogic
            //think about code reuse extract and make it common
            if (string.IsNullOrEmpty(poco.ContactPhone))
            {
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyProfile_ContactPhone,
                                  $"{nameof(poco.ContactPhone)}: Must correspond to a valid phone number (e.g. 416-555-1234)"));
            }
            else if (!poco.ContactPhone.EndsWith(".ca"))
            {
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyProfile_ContactPhone,
                                  $"{nameof(poco.ContactPhone)}: {poco.ContactPhone} Must correspond to a valid phone number (e.g. 416-555-1234)"));
            }
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}