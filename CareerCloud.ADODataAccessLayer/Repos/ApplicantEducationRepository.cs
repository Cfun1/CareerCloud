using System.Data;
using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Data.SqlClient;

namespace CareerCloud.ADODataAccessLayer;

public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
{
    public void Add(params ApplicantEducationPoco[] items)
    {
        DbConnection.Insert<ApplicantEducationPoco>(items);
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {//draft 
        var cnn = DbConnection.SqlCnn;
        using (SqlCommand cmd = cnn.CreateCommand())
        {
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cnn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                cnn.Close();
            }
        }
    }

    public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
    {
        return DbConnection.GetAllRecords<ApplicantEducationPoco>();
    }

    public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
    {
        IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
        return pocos.Where(where).FirstOrDefault();
    }

    public void Remove(params ApplicantEducationPoco[] items)
    {
        DbConnection.Delete<ApplicantEducationPoco>(items);
    }

    public void Update(params ApplicantEducationPoco[] items)
    {
        DbConnection.Update<ApplicantEducationPoco>(items);
    }

}