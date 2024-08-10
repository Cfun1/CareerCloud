using CareerCloud.BusinessLogicLayer.Helpers;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class CompanyJobSkillLogic : BaseLogic<CompanyJobSkillPoco>
{
    public CompanyJobSkillLogic(IDataRepository<CompanyJobSkillPoco> repository) : base(repository)
    {
    }

    public override void Add(CompanyJobSkillPoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    public override void Update(CompanyJobSkillPoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(CompanyJobSkillPoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (poco.Importance < 0)
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.CompanyJobSkill_Importance,
                                  $"{nameof(poco.Importance)}: {poco.Importance} cannot be less than 0"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}