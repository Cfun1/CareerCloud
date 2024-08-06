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
    protected override void Verify(ApplicantProfilePoco[] pocos)
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