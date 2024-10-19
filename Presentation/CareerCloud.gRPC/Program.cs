using System.Diagnostics;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Services;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.gRPC;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc();


        var connectionString =
            builder.Configuration.GetConnectionString("DataConnection");

        builder.Services.AddDbContext<CareerCloudContext>(options =>
        {
            options.UseSqlServer(connectionString!);
            if (builder.Environment.IsDevelopment())
            {
                ////options.UseLazyLoadingProxies();
                //todo: production? change this logging to serilog file logger
                options.AddInterceptors(new LoggingSaveChangesInterceptor());
                options.LogTo(msg => Debug.WriteLine(msg), LogLevel.Information);
            }
        });

        builder.Services.AddScoped(typeof(IDataRepository<>), typeof(EFGenericRepository<>));


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<ApplicantEducationService>();
        app.MapGrpcService<ApplicantJobApplicationService>();
        app.MapGrpcService<ApplicantProfileService>();
        app.MapGrpcService<CompanyDescriptionService>();
        app.MapGrpcService<CompanyJobEducationService>();
        app.MapGrpcService<CompanyJobService>();
        app.MapGrpcService<SecurityLoginService>();
        app.MapGrpcService<SecurityLoginsLogService>();
        app.MapGrpcService<SystemLanguageCodeService>();

        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}