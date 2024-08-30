using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
{
    public void Add(params CompanyJobPoco[] items)
    {
        DbConnection.Insert<CompanyJobPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<CompanyJobPoco>();
    }

    public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
    {
        IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params CompanyJobPoco[] items)
    {
        DbConnection.Delete<CompanyJobPoco>(items);
    }

    public void Update(params CompanyJobPoco[] items)
    {
        DbConnection.Update<CompanyJobPoco>(items);
    }

}