using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
{
    public void Add(params SecurityLoginsLogPoco[] items)
    {
        DbConnection.Insert<SecurityLoginsLogPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<SecurityLoginsLogPoco>();
    }

    public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
    {
        IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params SecurityLoginsLogPoco[] items)
    {
        DbConnection.Delete<SecurityLoginsLogPoco>(items);
    }

    public void Update(params SecurityLoginsLogPoco[] items)
    {
        DbConnection.Update<SecurityLoginsLogPoco>(items);
    }

}