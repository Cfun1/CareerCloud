using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class ApplicantResumeLogic : BaseLogic<ApplicantResumePoco>
{
    public ApplicantResumeLogic(IDataRepository<ApplicantResumePoco> repository) : base(repository)
    {
    }

    public override void Add(ApplicantResumePoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }

    public override void Update(ApplicantResumePoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(ApplicantResumePoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.Resume))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.ApplicantResume_Resume,
                                  $"{nameof(poco.Resume)} cannot be empty"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }
}