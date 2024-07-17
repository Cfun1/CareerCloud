using System.Data;
using System.Reflection;


namespace CareerCloud.ADODataAccessLayer;
public static class DataTableExtensions
{
    public static List<T> MapToListOf<T>(this DataTable dataTable) where T : new()
    {
        var pocos = new List<T>();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (DataRow row in dataTable.Rows)
        {
            if (row.HasErrors)  //todo? raise exception?
                continue;

            var item = new T();
            foreach (var property in properties)
            {
                var columnName = ReflectionHelpers.GetColumnFromProperty<T>(property.Name);

                if (!dataTable.HasColumn(columnName))
                    continue;

                var value = row[columnName];

                var propType = property.PropertyType;
                //handle nullable types
                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    propType = Nullable.GetUnderlyingType(propType);
                }

                //avoid Object cannot be cast from DBNull to other types exception
                property.SetValue(item, value == DBNull.Value ? null : Convert.ChangeType(value, propType!));
            }
            pocos.Add(item);
        }
        return pocos;
        //foreach (var poco in pocos)
        //    yield return poco;
    }

    // Extension method to check if a column exists in the data reader
    public static bool HasColumn(this DataTable dataTable, string columnName)
        => dataTable.Columns.Contains(columnName);
}
