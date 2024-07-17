using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Microsoft.IdentityModel.Tokens;

namespace CareerCloud.ADODataAccessLayer;
internal static class ReflectionHelpers
{
    /*** Retireve value of custom attribute Table from T type using refelction ***/
    internal static string? GetTableNameFrom<T>() where T : new() //IPoco,because of SystemCountryCode SystemLanguageCode
        => (typeof(T).GetCustomAttributes(typeof(TableAttribute), inherit: false)
                .FirstOrDefault() as TableAttribute)?.Name;

    /*** Retireve name and value of the first property having custom attribute "key" from T type using refelction ***/
    internal static (string, object) GetKeyPropertyFrom<T>(T poco) where T : new()
    {
        if (poco == null)
            throw new ArgumentNullException(nameof(poco));

        var keyProperty = typeof(T).GetProperties()
          .Where(prop => prop.GetCustomAttributes(typeof(KeyAttribute), false).Any()).FirstOrDefault();

        if (keyProperty is null)
            throw new KeyNotFoundException($"Property with key attribute not found for {typeof(T)}");

        var keyValue = GetPropertyValueOf(poco, keyProperty.Name);

        return (keyProperty.Name, keyValue);
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

    internal static IEnumerable<string> GetPropertiesNamesOf<T>(bool skipKey = false)
    {
        IEnumerable<string> propertiesNames;

        var properties = typeof(T).GetProperties().Where(prop => prop.GetCustomAttributes(typeof(ColumnAttribute), false).Any() &&
            (prop.GetCustomAttributes(typeof(KeyAttribute), false).IsNullOrEmpty() || !skipKey));
        propertiesNames = properties.Select(prop => prop.Name);

        if (propertiesNames == null)
            throw new Exception($"Unexpected null column attribute for {propertiesNames}");

        return propertiesNames;
    }

    internal static string GetColumnFromProperty<T>(string propName)
    {
        var prop = typeof(T).GetProperties().Where(prop => prop.Name == propName).FirstOrDefault();
        var temp = (ColumnAttribute)prop.GetCustomAttribute(typeof(ColumnAttribute));

        if (temp == null)
            throw new Exception($"Unexpected null column attribute for {prop.Name}");

        return temp.Name;
    }

}
