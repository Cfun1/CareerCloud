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
        #region ApplicantEducationPoco
        //ApplicantEducationPoco <<- (Applicant) -> ApplicantProfilePoco
        modelBuilder.Entity<ApplicantEducationPoco>()
          .HasOne(e => e.ApplicantProfile)
          .WithMany(e => e.ApplicantEducations)
          .HasForeignKey(e => e.Applicant)
          .IsRequired();
        #endregion


        #region ApplicantJobApplicationPoco
        //ApplicantJobApplicationPoco <<- () -> ApplicantProfilePoco
        modelBuilder.Entity<ApplicantJobApplicationPoco>()
          .HasOne(e => e.ApplicantProfile)
          .WithMany(e => e.ApplicantJobApplications)
          .HasForeignKey(e => e.Applicant)
          .IsRequired();

        //ApplicantJobApplicationPoco <<- () -> CompanyJobPoco
        modelBuilder.Entity<ApplicantJobApplicationPoco>()
          .HasOne(e => e.CompanyJob)
          .WithMany(e => e.ApplicantJobApplications)
          .HasForeignKey(e => e.Job)
          .IsRequired();
        #endregion


        #region ApplicantProfilePoco
        //ApplicantProfilePoco <<- () -> SecurityLoginPoco
        //note: should be one-to-one I believe: a single SecurityLogin reference a single applicant
        modelBuilder.Entity<ApplicantProfilePoco>()
          .HasOne(e => e.SecurityLogin)
          .WithMany(e => e.ApplicantProfiles)
          .HasForeignKey(e => e.Login)
          .IsRequired();

        //ApplicantProfilePoco <<- () -> SystemCountryCodePoco
        modelBuilder.Entity<ApplicantProfilePoco>()
          .HasOne(e => e.SystemCountryCode)
          .WithMany(e => e.ApplicantProfiles)
          .HasForeignKey(e => e.Country);
        #endregion


        #region ApplicantResumePoco
        //ApplicantResumePoco <<- () -> ApplicantProfilePoco
        modelBuilder.Entity<ApplicantResumePoco>()
          .HasOne(e => e.ApplicantProfile)
          .WithMany(e => e.ApplicantResumes)
          .HasForeignKey(e => e.Applicant)
          .IsRequired();
        #endregion


        #region ApplicantSkillPoco
        //ApplicantSkillPoco <<- () -> ApplicantProfilePoco
        modelBuilder.Entity<ApplicantSkillPoco>()
          .HasOne(e => e.ApplicantProfile)
          .WithMany(e => e.ApplicantSkills)
          .HasForeignKey(e => e.Applicant)
          .IsRequired();
        #endregion


        #region ApplicantWorkHistoryPoco
        //ApplicantWorkHistoryPoco <<- () -> ApplicantProfilePoco
        modelBuilder.Entity<ApplicantWorkHistoryPoco>()
          .HasOne(e => e.ApplicantProfile)
          .WithMany(e => e.ApplicantWorkHistorys)
          .HasForeignKey(e => e.Applicant);

        //ApplicantWorkHistoryPoco <<- () -> SystemCountryCodePoco
        modelBuilder.Entity<ApplicantWorkHistoryPoco>()
          .HasOne(e => e.SystemCountryCode)
          .WithMany(e => e.ApplicantWorkHistories)
          .HasForeignKey(e => e.CountryCode);
        #endregion


        #region CompanyDescriptionPoco
        //CompanyDescriptionPoco <<- () -> CompanyProfilePoco
        //note: many descriptions / company because in different languages
        modelBuilder.Entity<CompanyDescriptionPoco>()
          .HasOne(e => e.CompanyProfile)
          .WithMany(e => e.CompanyDescriptions)
          .HasForeignKey(e => e.Company)
          .IsRequired();

        //CompanyDescriptionPoco <<- () -> SystemLanguageCode
        modelBuilder.Entity<CompanyDescriptionPoco>()
          .HasOne(e => e.SystemLanguageCode)
          .WithMany(e => e.CompanyDescriptions)
          .HasForeignKey(e => e.LanguageId);
        #endregion


        #region CompanyJobPoco
        //CompanyJobPoco <<- () -> CompanyProfilePoco
        modelBuilder.Entity<CompanyJobPoco>()
          .HasOne(e => e.CompanyProfile)
          .WithMany(e => e.CompanyJobs)
          .HasForeignKey(e => e.Company)
          .IsRequired();
        #endregion


        #region CompanyJobDescriptionPoco
        //CompanyJobDescriptionPoco <<- () -> CompanyJobPoco
        modelBuilder.Entity<CompanyJobDescriptionPoco>()
          .HasOne(e => e.CompanyJob)
          .WithMany(e => e.CompanyJobDescriptions)
          .HasForeignKey(e => e.Job)
          .IsRequired();
        #endregion


        #region CompanyJobEducationPoco
        //CompanyJobEducationPoco <<- () -> CompanyJobPoco
        modelBuilder.Entity<CompanyJobEducationPoco>()
          .HasOne(e => e.CompanyJob)
          .WithMany(e => e.CompanyJobEducations)
          .HasForeignKey(e => e.Job)
          .IsRequired();
        #endregion


        #region CompanyJobSkillPoco
        //CompanyJobSkillPoco <<- () -> CompanyJobPoco
        modelBuilder.Entity<CompanyJobSkillPoco>()
          .HasOne(e => e.CompanyJob)
          .WithMany(e => e.CompanyJobSkills)
          .HasForeignKey(e => e.Job)
          .IsRequired();
        #endregion


        #region CompanyLocationPoco
        //CompanyLocationPoco <<- () -> CompanyProfilePoco
        modelBuilder.Entity<CompanyLocationPoco>()
          .HasOne(e => e.CompanyProfile)
          .WithMany(e => e.CompanyLocations)
          .HasForeignKey(e => e.Company)
          .IsRequired();

        //CompanyLocationPoco <<- () -> SystemCountryCodePoco
        modelBuilder.Entity<CompanyLocationPoco>()
          .HasOne(e => e.SystemCountryCode)
          .WithMany(e => e.CompanyLocations)
          .HasForeignKey(e => e.CountryCode)
          .IsRequired();
        #endregion


        #region SecurityLoginsLogPoco
        //SecurityLoginsLogPoco <<- () -> SecurityLoginPoco
        modelBuilder.Entity<SecurityLoginsLogPoco>()
          .HasOne(e => e.SecurityLogin)
          .WithMany(e => e.SecurityLoginsLogs)
          .HasForeignKey(e => e.Login)
          .IsRequired();
        #endregion


        #region SecurityLoginsRolePoco
        //SecurityLoginsRolePoco <<- () -> SecurityLoginPoco
        modelBuilder.Entity<SecurityLoginsRolePoco>()
          .HasOne(e => e.SecurityLogin)
          .WithMany(e => e.SecurityLoginsRoles)
          .HasForeignKey(e => e.Login)
          .IsRequired();

        //SecurityLoginsRolePoco <<- () -> SecurityRolePoco
        modelBuilder.Entity<SecurityLoginsRolePoco>()
          .HasOne(e => e.SecurityRole)
          .WithMany(e => e.SecurityLoginsRoles)
          .HasForeignKey(e => e.Login)
          .IsRequired();
        #endregion
    }
}
