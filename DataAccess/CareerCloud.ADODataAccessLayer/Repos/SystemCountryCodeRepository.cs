using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
{
    public void Add(params SystemCountryCodePoco[] items)
    {
        DbConnection.Insert<SystemCountryCodePoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<SystemCountryCodePoco>();
    }

    public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
    {
        IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params SystemCountryCodePoco[] items)
    {
        DbConnection.Delete<SystemCountryCodePoco>(items);
    }

    public void Update(params SystemCountryCodePoco[] items)
    {
        DbConnection.Update<SystemCountryCodePoco>(items);
    }

}