using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class CompanyJobEducationRepository : IDataRepository<CompanyJobEducationPoco>
{
    public void Add(params CompanyJobEducationPoco[] items)
    {
        DbConnection.Insert<CompanyJobEducationPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<CompanyJobEducationPoco>();
    }

    public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
    {
        IQueryable<CompanyJobEducationPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params CompanyJobEducationPoco[] items)
    {
        DbConnection.Delete<CompanyJobEducationPoco>(items);
    }

    public void Update(params CompanyJobEducationPoco[] items)
    {
        DbConnection.Update<CompanyJobEducationPoco>(items);
    }

}