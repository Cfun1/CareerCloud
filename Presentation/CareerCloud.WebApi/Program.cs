using System.Diagnostics;
using Asp.Versioning;
using CareerCloud.ADODataAccessLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.WebAPI.Extensions;
using CareerCloud.WebAPI.Swagger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;


var builder = WebApplication.CreateBuilder(args);
var dataAccessFramework = builder.Configuration.GetStringWithCheck("DataAccessFramework");
var apiVersionHeader = builder.Configuration.GetStringWithCheck("ApiVersionHeader");

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
#region builder
// Add services to the container.
builder.Services.AddControllers(options =>
{
    //used for filtering/bypass some properties considered required by validation
    //not needed anymore using DTO as an extra abstraction
    //options.Filters.Add<IgnoreTimeStampFilter>();
}
);

ConfigureApiVersionning();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
    {
        //used to (filter) enforce custom header parameter to be optional
        opt.OperationFilter<OptionalApiVersionHeaderParameter>(apiVersionHeader);
    }
);

ConfigureDataAccess();

ConfigureSwagger();

builder.Services.AddCors(p => p.AddPolicy(
    "CorsPolicy", build =>
    {
        build.AllowAnyOrigin().WithMethods("GET", "POST").AllowAnyHeader();
    }));

#endregion

var app = builder.Build();

#region app
if (app.Environment.IsDevelopment())
{
    UseSwagger();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion

#region Methods
WebApplicationBuilder ConfigureApiVersionning()
{

    builder.Services.AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        //options.DefaultApiVersion = new ApiVersion(1, 0);
        options.DefaultApiVersion = ApiVersion.Default;

        options.AssumeDefaultVersionWhenUnspecified = true;

        options.ApiVersionReader = new UrlSegmentApiVersionReader();
        //options.ApiVersionReader = new HeaderApiVersionReader(apiVersionHeader, apiVersionHeader);
    })
        //usefull for swagger
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV"; //Our format of our version number ��v�major[.minor][-status]�
            options.SubstituteApiVersionInUrl = true; //This will help us to resolve the ambiguity when there is a routing conflict due to routing template one or more end points are same.
        });
    return builder;
}

WebApplicationBuilder ConfigureSwagger()
{
    builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
         ConfigureSwaggerOptions>();
    return builder;
}

WebApplicationBuilder UseSwagger()
{
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            var descriptions = app.DescribeApiVersions();
            foreach (var description in descriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        }
        );
    return builder;
}

WebApplicationBuilder ConfigureDataAccess()
{
    switch (dataAccessFramework)
    {
        case "EF":
            builder.Services.AddScoped(typeof(IDataRepository<>), typeof(EFGenericRepository<>));
            break;

        case "ADO":
            var repos = new HashSet<KeyValuePair<Type, Type>>()
        {
            new(typeof(ApplicantEducationPoco), typeof(ApplicantEducationRepository)),
            new(typeof(ApplicantJobApplicationPoco), typeof(ApplicantJobApplicationRepository)),
            new(typeof(ApplicantProfilePoco), typeof(ApplicantProfileRepository)),
            new(typeof(ApplicantResumePoco), typeof(ApplicantResumeRepository)),
            new(typeof(ApplicantSkillPoco), typeof(ApplicantSkillRepository)),
            new(typeof(ApplicantWorkHistoryPoco), typeof(ApplicantWorkHistoryRepository)),
            new(typeof(CompanyDescriptionPoco), typeof(CompanyDescriptionRepository)),
            new(typeof(CompanyJobDescriptionPoco), typeof(CompanyJobDescriptionRepository)),
            new(typeof(CompanyJobEducationPoco), typeof(CompanyJobEducationRepository)),
            new(typeof(CompanyJobPoco), typeof(CompanyJobRepository)),
            new(typeof(CompanyJobSkillPoco), typeof(CompanyJobSkillRepository)),
            new(typeof(CompanyLocationPoco), typeof(CompanyLocationRepository)),
            new(typeof(CompanyProfilePoco), typeof(CompanyProfileRepository)),

            new(typeof(SecurityLoginPoco), typeof(SecurityLoginRepository)),
            new(typeof(SecurityLoginsLogPoco), typeof(SecurityLoginsLogRepository)),
            new(typeof(SecurityLoginsRolePoco), typeof(SecurityLoginsRoleRepository)),
            new(typeof(SecurityRolePoco), typeof(SecurityRoleRepository)),
            new(typeof(SystemCountryCodePoco), typeof(SystemCountryCodeRepository)),
            new(typeof(SystemLanguageCodePoco), typeof(SystemLanguageCodeRepository)),

        };

            foreach (var repo in repos)
            {
                // Dynamically (at runtime) create the generic interface type
                Type repositoryInterface = typeof(IDataRepository<>).MakeGenericType(repo.Key);
                builder.Services.AddScoped(repositoryInterface, repo.Value);
            }
            break;

        default:
            throw new InvalidOperationException("The required configuration 'DataAccessFramework' is missing in appsettings.json: supported frameworks 'EF' 'ADO'");
    }
    return builder;
}
#endregion