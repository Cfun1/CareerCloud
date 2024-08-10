using CareerCloud.BusinessLogicLayer.Helpers;
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

    public override void Update(ApplicantSkillPoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(ApplicantSkillPoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (poco.StartMonth > 12)
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.ApplicantSkill_StartMonth,
                                  $"{nameof(poco.StartMonth)}: {poco.StartMonth} Cannot be greater than 12"));

            if (poco.EndMonth > 12)
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.ApplicantSkill_EndMonth,
                                  $"{nameof(poco.EndMonth)}: {poco.EndMonth} Cannot be greater than 12"));

            if (poco.StartYear < 1900)
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.ApplicantSkill_StartYear,
                                  $"{nameof(poco.StartYear)}: {poco.StartYear} Cannot be less than 1900"));

            if (poco.EndYear < poco.StartYear)
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.ApplicantSkill_EndYear,
                                  $"{nameof(poco.EndYear)}: {poco.EndYear} Cannot be less than StartYear {poco.StartYear}"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}