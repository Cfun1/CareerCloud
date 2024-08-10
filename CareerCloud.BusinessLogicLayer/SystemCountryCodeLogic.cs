using CareerCloud.BusinessLogicLayer.Helpers;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class SystemCountryCodeLogic
{
    protected IDataRepository<SystemCountryCodePoco> _repository;

    public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
    {
        _repository = repository;
    }

    protected virtual void Verify(SystemCountryCodePoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.Code))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.SystemCountryCode_Code,
                                  $"{nameof(poco.Code)} Cannot be empty"));

            if (string.IsNullOrEmpty(poco.Name))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.SystemCountryCode_Name,
                                  $"{nameof(poco.Name)} Cannot be empty"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }

    public virtual SystemCountryCodePoco Get(string code)
    {
        return _repository.GetSingle(c => c.Code == code);
    }

    public virtual List<SystemCountryCodePoco> GetAll()
    {
        return _repository.GetAll().ToList();
    }

    public virtual void Add(SystemCountryCodePoco[] pocos)
    {
        Verify(pocos);
        _repository.Add(pocos);
    }

    public virtual void Update(SystemCountryCodePoco[] pocos)
    {
        Verify(pocos);
        _repository.Update(pocos);
    }

    public void Delete(SystemCountryCodePoco[] pocos)
    {
        _repository.Remove(pocos);
    }
}