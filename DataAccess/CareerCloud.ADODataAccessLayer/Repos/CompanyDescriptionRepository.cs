using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
{
    public void Add(params CompanyDescriptionPoco[] items)
    {
        DbConnection.Insert<CompanyDescriptionPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<CompanyDescriptionPoco>();
    }

    public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
    {
        IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params CompanyDescriptionPoco[] items)
    {
        DbConnection.Delete<CompanyDescriptionPoco>(items);
    }

    public void Update(params CompanyDescriptionPoco[] items)
    {
        DbConnection.Update<CompanyDescriptionPoco>(items);
    }

}