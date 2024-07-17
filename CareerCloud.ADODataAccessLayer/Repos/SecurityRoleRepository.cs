using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
{
    public void Add(params SecurityRolePoco[] items)
    {
        DbConnection.Insert<SecurityRolePoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<SecurityRolePoco>();
    }

    public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
    {
        IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params SecurityRolePoco[] items)
    {
        DbConnection.Delete<SecurityRolePoco>(items);
    }

    public void Update(params SecurityRolePoco[] items)
    {
        DbConnection.Update<SecurityRolePoco>(items);
    }

}