using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
{
    public void Add(params CompanyProfilePoco[] items)
    {
        DbConnection.Insert<CompanyProfilePoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<CompanyProfilePoco>();
    }

    public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
    {
        IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params CompanyProfilePoco[] items)
    {
        DbConnection.Delete<CompanyProfilePoco>(items);
    }

    public void Update(params CompanyProfilePoco[] items)
    {
        DbConnection.Update<CompanyProfilePoco>(items);
    }

}