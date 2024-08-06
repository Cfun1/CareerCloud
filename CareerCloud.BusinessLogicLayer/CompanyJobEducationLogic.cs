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
    protected override void Verify(CompanyJobEducationPoco[] pocos)
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