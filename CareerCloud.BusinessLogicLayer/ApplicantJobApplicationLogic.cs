using CareerCloud.BusinessLogicLayer.Helpers;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class ApplicantJobApplicationLogic : BaseLogic<ApplicantJobApplicationPoco>
{
    public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repository) : base(repository)
    {
    }

    public override void Add(ApplicantJobApplicationPoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }

    public override void Update(ApplicantJobApplicationPoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(ApplicantJobApplicationPoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (poco.ApplicationDate > DateTime.Today)
                validationExceptions.Add(new ValidationException(
                    ExceptionCodes.ApplicantJobApplication_ApplicationDate,
                    $"{nameof(poco.ApplicationDate)} {poco.ApplicationDate} cannot be greater than today"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}