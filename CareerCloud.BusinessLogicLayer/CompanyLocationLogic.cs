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
    protected override void Verify(CompanyLocationPoco[] pocos)
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