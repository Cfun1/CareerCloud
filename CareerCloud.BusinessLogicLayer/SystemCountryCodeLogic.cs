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
        return;
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
        foreach (SystemCountryCodePoco poco in pocos)
        {
            if (string.IsNullOrEmpty(poco.Code))
            {

            }
        }

        _repository.Add(pocos);
    }

    public virtual void Update(SystemCountryCodePoco[] pocos)
    {
        _repository.Update(pocos);
    }

    public void Delete(SystemCountryCodePoco[] pocos)
    {
        _repository.Remove(pocos);
    }
}