using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
{
    public void Add(params CompanyJobDescriptionPoco[] items)
    {
        DbConnection.Insert<CompanyJobDescriptionPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<CompanyJobDescriptionPoco>();
    }

    public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
    {
        IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params CompanyJobDescriptionPoco[] items)
    {
        DbConnection.Delete<CompanyJobDescriptionPoco>(items);
    }

    public void Update(params CompanyJobDescriptionPoco[] items)
    {
        DbConnection.Update<CompanyJobDescriptionPoco>(items);
    }

}