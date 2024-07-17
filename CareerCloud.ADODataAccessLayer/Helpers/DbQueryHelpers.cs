using System.Reflection;
using System.Text;
using Microsoft.Data.SqlClient;

namespace CareerCloud.ADODataAccessLayer;

internal static class DbQueryHelpers
{
    /*** Build the sql INSERT INTO string query, plus relevant columns value/parameters for T type using refelction ***/
    internal static (string, string, SqlParameter[]) PrepareInsertQueryFields<T>(
          bool skipKey = false, params T[] pocos)
        where T : new()
    {
        StringBuilder columnsNamesStr = new(string.Empty),
                      columnsValuesStr = new(string.Empty);

        List<string> columnsNames = new(), columnsValues = new();

        List<SqlParameter> sqlParameters = new();

        IEnumerable<string> propertiesName;
        propertiesName = ReflectionHelpers.GetPropertiesNamesOf<T>(skipKey);

        if (propertiesName.Count() == 0)
            throw new Exception("No properties found !?");

        ushort pocoCurrentNum = 1;  //don't name it index, not to confuse sicne index start from 0
        var pocosCount = pocos.Count();
        columnsNamesStr.Append("(");

        foreach (var poco in pocos)
        {
            columnsValuesStr.Append("(");

            foreach (var propertyName in propertiesName)
            {
                if (pocoCurrentNum == 1)
                {    //only build it once
                    columnsNames.Add(ReflectionHelpers.GetColumnFromProperty<T>(propertyName));
                }

                PropertyInfo? propInfo = typeof(T)?.GetProperty(propertyName);
                object? propertyValue = propInfo?.GetValue(poco);

                //flaw: if one object doesn't specify a column (null object) it will crash for inconsistency param count
                if (propertyValue == null)
                {
                    //should be done only first sicne we re building string only once doesn't make sense to try to fix issue later
                    columnsNames.Remove(ReflectionHelpers.GetColumnFromProperty<T>(propertyName));
                    continue;
                }

                string paramStr = $"@{propertyName}{pocoCurrentNum}";
                sqlParameters.Add(new SqlParameter(paramStr, propertyValue));
                columnsValues.Add(paramStr);
            }


            if (sqlParameters.Count % columnsNames.Count != 0 || columnsValues.Count % columnsNames.Count != 0)
                throw new InvalidOperationException($"Inconsistency: mismatch of count between columnsNames:{columnsNames.Count} and sqlParameters:{sqlParameters.Count % columnsNames.Count}");

            if (pocoCurrentNum == 1)    //only build name for first object
            {
                columnsNamesStr.Append(string.Join(", ", columnsNames));
                columnsNamesStr.Append(')');
            }

            columnsValuesStr.Append(string.Join(", ", columnsValues));
            columnsValuesStr.Append(')');

            if (pocoCurrentNum != pocosCount && pocosCount > 1) //not last, and not only one poco
                columnsValuesStr.Append(", ");

            columnsValues.Clear();
            pocoCurrentNum++;
        }
        return (columnsNamesStr.ToString(), columnsValuesStr.ToString(), sqlParameters.ToArray());
    }

    /*** Build the sql UPDATE string query, plus relevant columns value/parameters for T type using refelction ***/
    internal static (string, SqlParameter[]) PrepareUpdateQueryFields<T>(T poco, bool skipKey = false)
        where T : new()
    {
        List<SqlParameter> sqlParameters = new();
        StringBuilder queryStr = new StringBuilder();
        IEnumerable<string> propertiesName;

        propertiesName = ReflectionHelpers.GetPropertiesNamesOf<T>(skipKey);
        if (propertiesName.Count() == 0)
            throw new Exception("No properties found !?");

        foreach (var propertyName in propertiesName)
        {
            string ColumnName = ReflectionHelpers.GetColumnFromProperty<T>(propertyName);

            PropertyInfo? propInfo = typeof(T)?.GetProperty(propertyName);
            object? propertyValue = propInfo?.GetValue(poco);

            if (propertyValue == null)
                continue;

            sqlParameters.Add(new SqlParameter(propertyName, propertyValue));
            queryStr.Append($"{ColumnName}= @{propertyName}, ");
        }

        //more robust solution is using a list rather than string builder to check count consistency at the end
        queryStr.Remove(queryStr.Length - 2, 2);    //remove last ','
        return (queryStr.ToString(), sqlParameters.ToArray());
    }
}