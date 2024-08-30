using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class CompanyJobLogic : BaseLogic<CompanyJobPoco>
{
    public CompanyJobLogic(IDataRepository<CompanyJobPoco> repository) : base(repository)
    {
    }

    public override void Add(CompanyJobPoco[] pocos)
    {
        Verify(pocos);
        base.Add(pocos);
    }
    public override void Update(CompanyJobPoco[] pocos)
    {
        Verify(pocos);
        base.Update(pocos);
    }

    protected override void Verify(CompanyJobPoco[] pocos)
    {
        //List<ValidationException> validationExceptions = new List<ValidationException>();

        //foreach (var poco in pocos)
        //{
        //    if (poco)
        //        validationExceptions.Add(new ValidationException(
        //                          ExceptionCodes,
        //                          $" "));
        //}

        //if (validationExceptions.Count > 0)
        //    throw new AggregateException(validationExceptions);
    }
}