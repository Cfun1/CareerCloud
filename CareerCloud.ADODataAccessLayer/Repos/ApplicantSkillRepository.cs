using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
{
    public void Add(params ApplicantSkillPoco[] items)
    {
        DbConnection.Insert<ApplicantSkillPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<ApplicantSkillPoco>();
    }

    public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
    {
        IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params ApplicantSkillPoco[] items)
    {
        DbConnection.Delete<ApplicantSkillPoco>(items);
    }

    public void Update(params ApplicantSkillPoco[] items)
    {
        DbConnection.Update<ApplicantSkillPoco>(items);
    }

}