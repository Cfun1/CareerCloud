using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
{
    public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
    {
    }

    public override void Add(ApplicantSkillPoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    protected override void Verify(ApplicantSkillPoco[] pocos)
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