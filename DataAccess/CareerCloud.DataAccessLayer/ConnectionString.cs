using Microsoft.Extensions.Configuration;

namespace CareerCloud.DataAccessLayer;
public static class CommonDbConnection
{
    public static string? String
    {
        get
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            return root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
    }
}
