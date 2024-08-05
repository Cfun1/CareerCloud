//***********           TODOs
//              Improve code quality
//      //[PRIVACY_OMITTED]
//
//      1- Implement insert all/multiple records in one batch (batch processing ?)          [x]
//      2- Add appconfig instead of string                                                  [x]
//      3- Implement update multiple records based on a query: ie use MERGE
//      4- Implement transactions
//      4- Implement schemas names? if not default dbo
//      5- Implement exceptions handling rather than re-throw, custom exceptions
//      6- Check if code is compliant with S.O.L.I.D principles, refactor if necessary      [On going...]
//      7- Make SqlCnn singelton thread safe
//      8- Think about lazy loading using yield / change List to IEnumerator when possible for performance purpose
//      9- Better nullability checks

using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer;

public static class DbConnection
{
    //modified from string literal to appsentig after 15/07 class - readonly only ini once in the static ctor
    static readonly string? cnnStr;  //= @"Server=localhost\MSSQLDEV; Database=JOB_PORTAL_DB; Integrated Security=true; TrustServerCertificate=True; Application Name='ADO.NET //[PRIVACY_OMITTED] Demo App'";

    static DbConnection()
    {
        // avoids an exception when the test project is using System.Data.SqlClient instead of Microsoft.Data.SqlClient;
        System.Transactions.TransactionManager.ImplicitDistributedTransactions = true;

        var config = new ConfigurationBuilder();
        var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
        config.AddJsonFile(path, false);
        var root = config.Build();
        cnnStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
    }

    static SqlConnection _sqlCnn;
    public static SqlConnection SqlCnn => _sqlCnn ??= new SqlConnection(cnnStr);

    public static void Insert<T>(params T[] pocos) where T : new() //, IPoco
    {
        (string columnsNameStatement, string columnsVal, SqlParameter[] parameters) sqlInsertContext;

        if (pocos == null)
            throw new ArgumentNullException("Unexpected error: pocos should not be null");

        var sqlTableName = ReflectionHelpers.GetTableNameFrom<T>();
        if (sqlTableName == null)
            throw new Exception("Unexpected error: table attribute not found");

        sqlInsertContext = DbQueryHelpers.PrepareInsertQueryFields<T>(skipKey: false, pocos);

        using (SqlCommand cmd = SqlCnn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = $"INSERT INTO {sqlTableName} {sqlInsertContext.columnsNameStatement} VALUES{sqlInsertContext.columnsVal}";
            cmd.Parameters.AddRange(sqlInsertContext.parameters);

            try
            {
                SqlCnn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                SqlCnn.Close();
            }
        }
    }

    public static IList<T> GetAllRecords<T>() where T : new()//, IPoco
    {
        List<T> applicantProfiles = new List<T>();

        var sqlTableName = ReflectionHelpers.GetTableNameFrom<T>();
        if (sqlTableName == null)
            throw new Exception("Unexpected error: table attribute not found");

        string queryStr = $"SELECT * FROM {sqlTableName}";

        //[[connected::]] connected approach -- reader
        //[[connected::]] using (SqlCommand cmd = SqlCnn.CreateCommand())
        //[[connected::]] {
        //[[connected::]] cmd.CommandType = CommandType.Text;
        //[[connected::]]cmd.CommandText = queryStr;

        try
        {
            SqlCnn.Open();
            DataTable dt = new DataTable(); //DataSet: is a colelction of DataTable, complex could hold a replica of DB
            using (SqlDataAdapter adapter = new SqlDataAdapter(queryStr, SqlCnn))
                adapter.Fill(dt);
            applicantProfiles = dt.MapToListOf<T>();

            //[[connected::]]using (var reader = cmd.ExecuteReader())
            //[[connected::]]{
            //[[connected::]]    applicantProfiles = reader.MapToList<T>();
            //[[connected::]]}
            return applicantProfiles;
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            //[[connected::]]      cmd.Dispose();
            SqlCnn.Close();
        }
        //[[connected::]]  }
    }

    public static void Delete<T>(params T[] pocos) where T : new() //, IPoco
    {
        (string Name, object Value) sqlKeyTuple;

        var sqlTableName = ReflectionHelpers.GetTableNameFrom<T>();
        if (sqlTableName == null)
            throw new Exception("Unexpected error: table attribute not found");

        foreach (var poco in pocos)
        {
            try
            {
                sqlKeyTuple = ReflectionHelpers.GetKeyPropertyFrom<T>(poco);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }

            using (SqlCommand cmd = SqlCnn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"DELETE FROM {sqlTableName} WHERE {sqlKeyTuple.Name} = @{sqlKeyTuple.Name}";
                cmd.Parameters.AddWithValue("@" + sqlKeyTuple.Name, sqlKeyTuple.Value);

                try
                {
                    SqlCnn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw;
                }
                finally
                {
                    cmd.Dispose();
                    SqlCnn.Close();
                }
            }
        }
    }

    public static void Update<T>(params T[] pocos) where T : new() //, IPoco
    {
        (string QueryStr, SqlParameter[] parameters) sqlUpdateContext;

        using (SqlCommand cmd = SqlCnn.CreateCommand())
        {
            cmd.CommandType = CommandType.Text;

            var sqlTableName = ReflectionHelpers.GetTableNameFrom<T>();
            if (sqlTableName == null)
                throw new Exception("Unexpected error: table attribute not found");

            try
            {
                foreach (var poco in pocos)
                {
                    if (poco is null)
                        throw new ArgumentNullException(nameof(poco));

                    var (sqlKeyColumnName, sqlKeyColumnValue) = ReflectionHelpers.GetKeyPropertyFrom<T>(poco);
                    sqlUpdateContext = DbQueryHelpers.PrepareUpdateQueryFields<T>(poco, skipKey: true);

                    cmd.CommandText = $"UPDATE {sqlTableName} SET {sqlUpdateContext.QueryStr} WHERE {sqlKeyColumnName} = @{sqlKeyColumnName}";
                    cmd.Parameters.AddRange(sqlUpdateContext.parameters);
                    //because id is skipped in columns sqlUpdateContext.parameters
                    cmd.Parameters.AddWithValue($"{sqlKeyColumnName}", sqlKeyColumnValue);
                }

                SqlCnn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (SqlException ex)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
                SqlCnn.Close();
            }
        }
    }
}