using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
{
    public void Add(params ApplicantJobApplicationPoco[] items)
    {
        DbConnection.Insert<ApplicantJobApplicationPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<ApplicantJobApplicationPoco>();
    }

    public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
    {
        IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params ApplicantJobApplicationPoco[] items)
    {
        DbConnection.Delete<ApplicantJobApplicationPoco>(items);
    }

    public void Update(params ApplicantJobApplicationPoco[] items)
    {
        DbConnection.Update<ApplicantJobApplicationPoco>(items);
    }

}