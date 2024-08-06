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
        return;
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
        foreach (SystemLanguageCodePoco poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.LanguageID))
            {

            }
        }

        _repository.Add(pocos);
    }

    public virtual void Update(SystemLanguageCodePoco[] pocos)
    {
        _repository.Update(pocos);
    }

    public void Delete(SystemLanguageCodePoco[] pocos)
    {
        _repository.Remove(pocos);
    }
}