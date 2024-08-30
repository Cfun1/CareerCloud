using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
{
    public void Add(params SystemLanguageCodePoco[] items)
    {
        DbConnection.Insert<SystemLanguageCodePoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<SystemLanguageCodePoco>();
    }

    public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
    {
        IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params SystemLanguageCodePoco[] items)
    {
        DbConnection.Delete<SystemLanguageCodePoco>(items);
    }

    public void Update(params SystemLanguageCodePoco[] items)
    {
        DbConnection.Update<SystemLanguageCodePoco>(items);
    }
}