using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.BusinessLogicLayer;

public class ApplicantProfileLogic : BaseLogic<ApplicantProfilePoco>
{
    public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
    {
    }

    public override void Add(ApplicantProfilePoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    public override void Update(ApplicantProfilePoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(ApplicantProfilePoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (poco.CurrentSalary < 0)
                validationExceptions.Add(new ValidationException(
                    ExceptionCodes.ApplicantProfile_CurrentSalary,
                    $"{nameof(poco.CurrentSalary)}: {poco.CurrentSalary} cannot be negative"));

            if (poco.CurrentRate < 0)
                validationExceptions.Add(new ValidationException(
                    ExceptionCodes.ApplicantProfile_CurrentRate,
                    $"{nameof(poco.CurrentRate)}: {poco.CurrentRate} cannot be negative"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}