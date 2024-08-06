using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class SecurityRoleLogic : BaseLogic<SecurityRolePoco>
{
    public SecurityRoleLogic(IDataRepository<SecurityRolePoco> repository) : base(repository)
    {
    }

    public override void Add(SecurityRolePoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    protected override void Verify(SecurityRolePoco[] pocos)
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