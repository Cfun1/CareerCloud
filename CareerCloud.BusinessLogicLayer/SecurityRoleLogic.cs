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
    public override void Update(SecurityRolePoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(SecurityRolePoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.Role))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.SecurityRole_Role,
                                  $"{nameof(poco.Role)} Cannot be empty"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}