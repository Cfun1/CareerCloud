using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess;

public class CareerCloudContext : DbContext
{
    public DbSet<ApplicantEducationPoco> ApplicantEducation;

    public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplication;

    public DbSet<ApplicantProfilePoco> ApplicantProfile;

    public DbSet<ApplicantResumePoco> ApplicantResume;

    public DbSet<ApplicantSkillPoco> ApplicantSkill;

    public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory;

    public DbSet<CompanyDescriptionPoco> CompanyDescription;

    public DbSet<CompanyJobDescriptionPoco> CompanyJobDescription;

    public DbSet<CompanyJobEducationPoco> CompanyJobEducation;

    public DbSet<CompanyJobPoco> CompanyJob;

    public DbSet<CompanyJobsDescriptionPoco> CompanyJobsDescription;

    public DbSet<CompanyJobSkillPoco> CompanyJobSkill;

    public DbSet<CompanyLocationPoco> CompanyLocation;

    public DbSet<CompanyProfilePoco> CompanyProfile;

    public DbSet<SecurityLoginPoco> SecurityLogin;

    public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog;

    public DbSet<SecurityLoginsRolePoco> SecurityLoginsRole;

    public DbSet<SecurityRolePoco> SecurityRole;

    public DbSet<SystemCountryCodePoco> SystemCountryCode;

    public DbSet<SystemLanguageCodePoco> SystemLanguageCode;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(DataAccessLayer.CommonDbConnection.String);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Describe later foreign keys relationship*/
    }
}
