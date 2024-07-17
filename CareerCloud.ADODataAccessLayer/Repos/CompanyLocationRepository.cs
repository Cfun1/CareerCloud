using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
{
    public void Add(params CompanyLocationPoco[] items)
    {
        DbConnection.Insert<CompanyLocationPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<CompanyLocationPoco>();
    }

    public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
    {
        IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params CompanyLocationPoco[] items)
    {
        DbConnection.Delete<CompanyLocationPoco>(items);
    }

    public void Update(params CompanyLocationPoco[] items)
    {
        DbConnection.Update<CompanyLocationPoco>(items);
    }

}