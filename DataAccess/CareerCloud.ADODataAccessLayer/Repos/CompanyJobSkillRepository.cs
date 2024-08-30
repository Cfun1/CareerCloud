using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
{
    public void Add(params CompanyJobSkillPoco[] items)
    {
        DbConnection.Insert<CompanyJobSkillPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<CompanyJobSkillPoco>();
    }

    public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
    {
        IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params CompanyJobSkillPoco[] items)
    {
        DbConnection.Delete<CompanyJobSkillPoco>(items);
    }

    public void Update(params CompanyJobSkillPoco[] items)
    {
        DbConnection.Update<CompanyJobSkillPoco>(items);
    }

}