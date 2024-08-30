using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;

namespace CareerCloud.ADODataAccessLayer;
internal static class ReflectionHelpers
{
    /*** Retrieve value of custom attribute Table from T type using reflection ***/
    internal static string? GetTableNameFrom<T>() where T : new() //IPoco,because of SystemCountryCode SystemLanguageCode
        => (typeof(T).GetCustomAttributes(typeof(TableAttribute), inherit: false)
                .FirstOrDefault() as TableAttribute)?.Name;

    /*** Retrieve name and value of the first property having custom attribute "key" from T type using reflection ***/
    internal static (string, object) GetKeyPropertyNameValueFrom<T>(T poco) where T : new()
    {
        if (poco == null)
            throw new ArgumentNullException(nameof(poco));

        var keyPropertyName = GetKeyPropertyNameFrom<T>();
        var keyValue = GetPropertyValueOf<T>(poco, keyPropertyName);

        return (keyPropertyName, keyValue);
    }

    internal static string GetKeyPropertyNameFrom<T>() where T : new()
    {
        var keyProperty = typeof(T).GetProperties()
                        .FirstOrDefault(prop =>
                            prop.GetCustomAttributes(typeof(KeyAttribute), false).Any());

        if (keyProperty is null)
            throw new KeyNotFoundException($"Property with key attribute not found for {typeof(T)}");

        return keyProperty.Name;
    }

    internal static object? GetPropertyValueOf<T>(T poco, string propertyName) where T : new()
    {
        if (poco is null)
            throw new ArgumentNullException(nameof(poco));

        PropertyInfo? propInfo = typeof(T)?.GetProperty(propertyName);
        if (propInfo is null)
            throw new ArgumentNullException($"Unexpected Property info is null {typeof(T)}");

        return propInfo?.GetValue(poco);
    }

    internal static Type? GetPropertyTypeOf<T>(string propertyName) where T : new()
    {
        PropertyInfo? propInfo = typeof(T)?.GetProperty(propertyName);
        if (propInfo is null)
            throw new ArgumentNullException($"Unexpected Property info is null {typeof(T)}");

        var propType = propInfo.PropertyType;
        //handle nullable types
        if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
        {
            propType = Nullable.GetUnderlyingType(propType);
        }
        return propType;
    }

    internal static IEnumerable<string> GetPropertiesNamesOf<T>(bool skipKey = false)
    {
        IEnumerable<string> propertiesNames;

        var properties = typeof(T).GetProperties()
            .Where(prop => prop.GetCustomAttributes(typeof(ColumnAttribute), false).Any()
            && (prop.GetCustomAttributes(typeof(KeyAttribute), false).IsNullOrEmpty() || !skipKey));

        try
        {
            propertiesNames = properties.Select(prop => prop.Name);
        }
        catch (ArgumentNullException ex)
        {
            throw new Exception($"Unexpected null source or selector: {ex.ParamName} {ex.Message}");
        }

        if (propertiesNames == null)
            throw new Exception($"Unexpected null column attribute for {propertiesNames}");

        return propertiesNames;
    }

    internal static string GetColumnFromProperty<T>(string propName)
    {
        var prop = typeof(T).GetProperties().First(prop => prop.Name == propName);
        if (prop == null)
            throw new InvalidOperationException();

        var temp = (ColumnAttribute?)prop.GetCustomAttribute(typeof(ColumnAttribute));

        if (temp == null || temp.Name == null)
            return string.Empty;
        //commented out because the properties added in the pocos for EF introduced an exception here
        //throw new Exception($"Unexpected null column attribute for {prop.Name}");

        return temp.Name;
    }
}
