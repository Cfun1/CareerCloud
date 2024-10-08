using CareerCloud.gRPC.Services;

namespace CareerCloud.gRPC;
public class Program
{
    /*
     * generate .proto service code + clas code for this
        ApplicantEducationPoco.cs
        ApplicantJobApplicationPoco.cs
        ApplicantProfilePoco.cs
        CompanyDescriptionPoco.cs
        CompanyJobEducationPoco.cs
        CompanyJobPoco.cs
        SecurityLoginPoco.cs
        SecurityLoginsLogPoco.cs
        SystemLanguageCodePoco.cs
    */
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddGrpc();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGrpcService<GreeterService>();
        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}