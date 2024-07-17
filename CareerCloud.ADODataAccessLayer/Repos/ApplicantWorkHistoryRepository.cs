using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
{
    public void Add(params ApplicantWorkHistoryPoco[] items)
    {
        DbConnection.Insert<ApplicantWorkHistoryPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<ApplicantWorkHistoryPoco>();
    }

    public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
    {
        IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params ApplicantWorkHistoryPoco[] items)
    {
        DbConnection.Delete<ApplicantWorkHistoryPoco>(items);
    }

    public void Update(params ApplicantWorkHistoryPoco[] items)
    {
        DbConnection.Update<ApplicantWorkHistoryPoco>(items);
    }

}