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
    protected override void Verify(CompanyProfilePoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (poco)
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes,
                                  $" "));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}