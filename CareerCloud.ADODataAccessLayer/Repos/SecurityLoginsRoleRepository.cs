using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
{
    public void Add(params SecurityLoginsRolePoco[] items)
    {
        DbConnection.Insert<SecurityLoginsRolePoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<SecurityLoginsRolePoco>();
    }

    public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
    {
        IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params SecurityLoginsRolePoco[] items)
    {
        DbConnection.Delete<SecurityLoginsRolePoco>(items);
    }

    public void Update(params SecurityLoginsRolePoco[] items)
    {
        DbConnection.Update<SecurityLoginsRolePoco>(items);
    }

}