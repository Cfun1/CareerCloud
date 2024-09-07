using Asp.Versioning;
using CareerCloud.ADODataAccessLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using CareerCloud.Web.Api;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;


var builder = WebApplication.CreateBuilder(args);
var dataAccessFramework = builder.Configuration.GetValue<string>("DataAccessFramework");


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
builder.Services.AddSwaggerGen();

ConfigureDataAccess();

ConfigureSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    UseSwagger();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


#region Methods
void ConfigureApiVersionning()
{

    builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
        //usefull for swagger
        .AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV"; //Our format of our version number “‘v’major[.minor][-status]”
            options.SubstituteApiVersionInUrl = true; //This will help us to resolve the ambiguity when there is a routing conflict due to routing template one or more end points are same.
        });
}

void ConfigureSwagger()
{
    builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
         ConfigureSwaggerOptions>();
}

void UseSwagger()
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
}

void ConfigureDataAccess()
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
                // Dynamically create the generic interface type
                Type repositoryInterface = typeof(IDataRepository<>).MakeGenericType(repo.Key);
                builder.Services.AddScoped(repositoryInterface, repo.Value);
            }
            break;

        default:
            throw new InvalidOperationException("The required configuration 'DataAccessFramework' is missing in appsettings.json: supported frameworks 'EF' 'ADO'");
    }
}
#endregion