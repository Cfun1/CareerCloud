using CareerCloud.BusinessLogicLayer.Helpers;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer;

public class SystemLanguageCodeLogic
{
    protected IDataRepository<SystemLanguageCodePoco> _repository;

    public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
    {
        _repository = repository;
    }

    protected virtual void Verify(SystemLanguageCodePoco[] pocos)
    {
        List<ValidationException> validationExceptions = new List<ValidationException>();

        foreach (var poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.LanguageID))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.SystemLanguageCode_LanguageID,
                                  $"{nameof(poco.LanguageID)} Cannot be empty"));

            if (string.IsNullOrEmpty(poco.Name))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.SystemLanguageCode_Name,
                                  $"{nameof(poco.Name)} Cannot be empty"));

            if (string.IsNullOrEmpty(poco.NativeName))
                validationExceptions.Add(new ValidationException(
                                  ExceptionCodes.SystemLanguageCode_NativeName,
                                  $"{nameof(poco.NativeName)} Cannot be empty"));
        }

        if (validationExceptions.Count > 0)
            throw new AggregateException(validationExceptions);
    }

    public virtual SystemLanguageCodePoco Get(string languageID)
    {
        return _repository.GetSingle(c => c.LanguageID == languageID);
    }

    public virtual List<SystemLanguageCodePoco> GetAll()
    {
        return _repository.GetAll().ToList();
    }

    public virtual void Add(SystemLanguageCodePoco[] pocos)
    {
        Verify(pocos);
        _repository.Add(pocos);
    }

    public virtual void Update(SystemLanguageCodePoco[] pocos)
    {
        Verify(pocos);
        _repository.Update(pocos);
    }

    public void Delete(SystemLanguageCodePoco[] pocos)
    {
        _repository.Remove(pocos);
    }
}