using System.Diagnostics;
using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CareerCloud.EntityFrameworkDataAccess;
//TODO:
//implement types for better optimization: .HasColumnType("varchar(200)")
public class CareerCloudContext : DbContext
{
    public DbSet<ApplicantEducationPoco> ApplicantEducation { get; set; }

    public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplication { get; set; }

    public DbSet<ApplicantProfilePoco> ApplicantProfile { get; set; }

    public DbSet<ApplicantResumePoco> ApplicantResume { get; set; }

    public DbSet<ApplicantSkillPoco> ApplicantSkill { get; set; }

    public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }

    public DbSet<CompanyDescriptionPoco> CompanyDescription { get; set; }

    public DbSet<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }

    public DbSet<CompanyJobEducationPoco> CompanyJobEducation { get; set; }

    public DbSet<CompanyJobPoco> CompanyJob { get; set; }

    public DbSet<CompanyJobDescriptionPoco> CompanyJobsDescription { get; set; }

    public DbSet<CompanyJobSkillPoco> CompanyJobSkill { get; set; }

    public DbSet<CompanyLocationPoco> CompanyLocation { get; set; }

    public DbSet<CompanyProfilePoco> CompanyProfile { get; set; }

    public DbSet<SecurityLoginPoco> SecurityLogin { get; set; }

    public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }

    public DbSet<SecurityLoginsRolePoco> SecurityLoginsRole { get; set; }

    public DbSet<SecurityRolePoco> SecurityRole { get; set; }

    public DbSet<SystemCountryCodePoco> SystemCountryCode { get; set; }

    public DbSet<SystemLanguageCodePoco> SystemLanguageCode { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(DataAccessLayer.CommonDbConnection.String)
            .LogTo(msg => Debug.WriteLine(msg), LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<CompanyDescriptionPoco>()
        //   .HasOne(p => p.SystemLanguageCode)
        //   .WithOne(a => a.CompanyDescription)
        //   .HasForeignKey<SystemLanguageCodePoco>(a => a.LanguageID);

        //modelBuilder.Entity<SystemLanguageCodePoco>()
        //    .HasOne(x => x.CompanyDescription)
        //    .WithOne(x => x.SystemLanguageCode)
        //.HasForeignKey<CompanyDescriptionPoco>(x => x.LanguageId)
        ;//.HasPrincipalKey(c => c.)
        /* Describe later foreign keys relationship*/
    }
}
