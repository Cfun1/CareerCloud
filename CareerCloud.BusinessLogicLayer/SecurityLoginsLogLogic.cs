using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class SecurityLoginsLogLogic : BaseLogic<SecurityLoginsLogPoco>
{
    public SecurityLoginsLogLogic(IDataRepository<SecurityLoginsLogPoco> repository) : base(repository)
    {
    }

    public override void Add(SecurityLoginsLogPoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    protected override void Verify(SecurityLoginsLogPoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (poco)
                validationExceptions.Add(new ValidationException(110, ""));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}