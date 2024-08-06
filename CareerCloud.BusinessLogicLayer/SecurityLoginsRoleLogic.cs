using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class SecurityLoginsRoleLogic : BaseLogic<SecurityLoginsRolePoco>
{
    public SecurityLoginsRoleLogic(IDataRepository<SecurityLoginsRolePoco> repository) : base(repository)
    {
    }

    public override void Add(SecurityLoginsRolePoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    protected override void Verify(SecurityLoginsRolePoco[] pocos)
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