using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
{
    public void Add(params ApplicantProfilePoco[] items)
    {
        DbConnection.Insert<ApplicantProfilePoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<ApplicantProfilePoco>();
    }

    public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
    {
        IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params ApplicantProfilePoco[] items)
    {
        DbConnection.Delete<ApplicantProfilePoco>(items);
    }

    public void Update(params ApplicantProfilePoco[] items)
    {
        DbConnection.Update<ApplicantProfilePoco>(items);
        //  DbConnection.UpdateMerge<ApplicantProfilePoco>(items);
    }
}