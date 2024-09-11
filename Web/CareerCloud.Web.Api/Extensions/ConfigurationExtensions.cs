namespace CareerCloud.Web.Api.Extensions;

internal static class ConfigurationExtensions
{
    public static string GetStringWithCheck(this IConfiguration configuration, string key)
    {
        var value = configuration.GetValueWithCheck<string>(key);
        if (string.IsNullOrEmpty(value))
            throw new InvalidOperationException($"Configuration key '{key}' is not found.");

        return value;
    }

    public static T GetValueWithCheck<T>(this IConfiguration configuration, string key)
    {
        var value = configuration.GetValue<T>(key);
        if (value is null || EqualityComparer<T>.Default.Equals(value, default))
            throw new InvalidOperationException($"Configuration key '{key}' is not found.");

        return value;
    }
}