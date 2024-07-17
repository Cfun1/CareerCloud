using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
{
    public void Add(params SecurityLoginPoco[] items)
    {
        DbConnection.Insert<SecurityLoginPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<SecurityLoginPoco>();
    }

    public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
    {
        IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params SecurityLoginPoco[] items)
    {
        DbConnection.Delete<SecurityLoginPoco>(items);
    }

    public void Update(params SecurityLoginPoco[] items)
    {
        DbConnection.Update<SecurityLoginPoco>(items);
    }

}