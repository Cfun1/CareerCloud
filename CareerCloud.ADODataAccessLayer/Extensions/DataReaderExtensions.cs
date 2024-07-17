using System.Reflection;
using Microsoft.Data.SqlClient;

namespace CareerCloud.ADODataAccessLayer;

public static class DataReaderExtensions
{
    //use access by name [] instead of using GetType() methods of reader, more flexible for generic approach
    public static List<T> MapToListOf<T>(this SqlDataReader reader) where T : new()
    {
        var results = new List<T>();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        while (reader.Read())
        {
            var item = new T();
            foreach (var property in properties)
            {
                var columnName = ReflectionHelpers.GetColumnFromProperty<T>(property.Name);
                if (!reader.HasColumn(columnName))// || reader[columnName] == DBNull.Value)
                    continue;

                var value = reader[columnName];

                var propType = property.PropertyType;
                //handle nullable types
                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    propType = Nullable.GetUnderlyingType(propType);
                }

                //avoid Object cannot be cast from DBNull to other types exception
                property.SetValue(item, value == DBNull.Value ? null : Convert.ChangeType(value, propType!));
            }
            results.Add(item);
        }

        return results;
    }

    // Extension method to check if a column exists in the data reader
    public static bool HasColumn(this SqlDataReader reader, string columnName)
    {
        for (int i = 0; i < reader.FieldCount; i++)
        {
            if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }
}
