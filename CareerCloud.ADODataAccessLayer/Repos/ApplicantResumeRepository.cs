using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;


namespace CareerCloud.ADODataAccessLayer;

public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
{
    public void Add(params ApplicantResumePoco[] items)
    {
        DbConnection.Insert<ApplicantResumePoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<ApplicantResumePoco>();
    }

    public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
    {
        IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params ApplicantResumePoco[] items)
    {
        DbConnection.Delete<ApplicantResumePoco>(items);
    }

    public void Update(params ApplicantResumePoco[] items)
    {
        DbConnection.Update<ApplicantResumePoco>(items);
    }

}