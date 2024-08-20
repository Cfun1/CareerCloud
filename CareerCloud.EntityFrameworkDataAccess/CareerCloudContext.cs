using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;

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
        optionsBuilder
                    .UseSqlServer(DataAccessLayer.CommonDbConnection.String)
                    .UseLazyLoadingProxies();
        //.LogTo(msg => Debug.WriteLine(msg), LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //no need for IsRequired(true/false) EF will make assumption based on nullability of type
        #region ApplicantProfilePoco
        //ApplicantProfilePoco <- (Applicant) ->> ApplicantEducations
        modelBuilder.Entity<ApplicantProfilePoco>()
          .HasMany(e => e.ApplicantEducations)
          .WithOne(e => e.ApplicantProfile)
          .HasForeignKey(e => e.Applicant);

        //note: should be one-to-one I believe: a single SecurityLogin reference a single applicant
        //ApplicantProfilePoco <<- (Login) -> SecurityLoginPoco
        modelBuilder.Entity<ApplicantProfilePoco>()
          .HasOne(e => e.SecurityLogin)
          .WithMany(e => e.ApplicantProfiles)
          .HasForeignKey(e => e.Login);

        //ApplicantProfilePoco <- (Applicant) ->> ApplicantWorkHistoryPoco
        modelBuilder.Entity<ApplicantProfilePoco>()
          .HasMany(e => e.ApplicantWorkHistorys)
          .WithOne(e => e.ApplicantProfile)
          .HasForeignKey(e => e.Applicant);

        //ApplicantProfilePoco  <- (Applicant) ->> ApplicantJobApplicationPoco
        modelBuilder.Entity<ApplicantProfilePoco>()
          .HasMany(e => e.ApplicantJobApplications)
          .WithOne(e => e.ApplicantProfile)
          .HasForeignKey(e => e.Applicant);

        //ApplicantProfilePoco  <- (Applicant) ->> ApplicantSkillPoco
        modelBuilder.Entity<ApplicantProfilePoco>()
          .HasMany(e => e.ApplicantSkills)
          .WithOne(e => e.ApplicantProfile)
          .HasForeignKey(e => e.Applicant);

        // ApplicantProfilePoco <- (Applicant) ->> ApplicantResumePoco 
        modelBuilder.Entity<ApplicantProfilePoco>()
          .HasMany(e => e.ApplicantResumes)
          .WithOne(e => e.ApplicantProfile)
          .HasForeignKey(e => e.Applicant);
        #endregion


        #region SystemCountryCodePoco
        //SystemCountryCodePoco <- (Country) ->> ApplicantProfilePoco
        modelBuilder.Entity<SystemCountryCodePoco>()
          .HasMany(e => e.ApplicantProfiles)
          .WithOne(e => e.SystemCountryCode)
          .HasForeignKey(e => e.Country);

        //SystemCountryCodePoco <- (CountryCode) ->> ApplicantWorkHistoryPoco
        modelBuilder.Entity<SystemCountryCodePoco>()
          .HasMany(e => e.ApplicantWorkHistories)
          .WithOne(e => e.SystemCountryCode)
          .HasForeignKey(e => e.CountryCode);

        //SystemCountryCodePoco  <- (CountryCode) ->> CompanyLocationPoco
        modelBuilder.Entity<SystemCountryCodePoco>()
          .HasMany(e => e.CompanyLocations)
          .WithOne(e => e.SystemCountryCode)
          .HasForeignKey(e => e.CountryCode);
        #endregion


        #region CompanyDescriptionPoco
        //CompanyDescriptionPoco <<- (Company) -> CompanyProfilePoco
        //note: many descriptions / company because in different languages
        modelBuilder.Entity<CompanyDescriptionPoco>()
          .HasOne(e => e.CompanyProfile)
          .WithMany(e => e.CompanyDescriptions)
          .HasForeignKey(e => e.Company);

        //CompanyDescriptionPoco <<- (LanguageId) -> SystemLanguageCode
        modelBuilder.Entity<CompanyDescriptionPoco>()
          .HasOne(e => e.SystemLanguageCode)
          .WithMany(e => e.CompanyDescriptions)
          .HasForeignKey(e => e.LanguageId);
        #endregion


        #region CompanyJobPoco
        //CompanyJobPoco <- (Job) ->> ApplicantJobApplicationPoco
        modelBuilder.Entity<CompanyJobPoco>()
          .HasMany(e => e.ApplicantJobApplications)
          .WithOne(e => e.CompanyJob)
          .HasForeignKey(e => e.Job);

        //CompanyJobPoco <- (Job) ->> CompanyJobDescriptionPoco
        modelBuilder.Entity<CompanyJobPoco>()
          .HasMany(e => e.CompanyJobDescriptions)
          .WithOne(e => e.CompanyJob)
          .HasForeignKey(e => e.Job);

        //CompanyJobPoco <- (Job) ->> CompanyJobEducationPoco 
        modelBuilder.Entity<CompanyJobPoco>()
          .HasMany(e => e.CompanyJobEducations)
          .WithOne(e => e.CompanyJob)
          .HasForeignKey(e => e.Job);

        //CompanyJobPoco  <- (Job) ->> CompanyJobSkillPoco
        modelBuilder.Entity<CompanyJobPoco>()
          .HasMany(e => e.CompanyJobSkills)
          .WithOne(e => e.CompanyJob)
          .HasForeignKey(e => e.Job);
        #endregion


        #region CompanyProfilePoco
        //CompanyProfilePoco <- (Company) ->> CompanyLocationPoco
        modelBuilder.Entity<CompanyProfilePoco>()
          .HasMany(e => e.CompanyLocations)
          .WithOne(e => e.CompanyProfile)
          .HasForeignKey(e => e.Company);

        //CompanyProfilePoco  <- (Company) ->> CompanyJobPoco
        modelBuilder.Entity<CompanyProfilePoco>()
          .HasMany(e => e.CompanyJobs)
          .WithOne(e => e.CompanyProfile)
          .HasForeignKey(e => e.Company);
        #endregion


        #region SecurityLoginsLogPoco
        //SecurityLoginsLogPoco <<- (Login) -> SecurityLoginPoco
        modelBuilder.Entity<SecurityLoginsLogPoco>()
          .HasOne(e => e.SecurityLogin)
          .WithMany(e => e.SecurityLoginsLogs)
          .HasForeignKey(e => e.Login);
        #endregion


        #region SecurityLoginsRolePoco
        //SecurityLoginsRolePoco <<- (Login) -> SecurityLoginPoco
        modelBuilder.Entity<SecurityLoginsRolePoco>()
          .HasOne(e => e.SecurityLogin)
          .WithMany(e => e.SecurityLoginsRoles)
          .HasForeignKey(e => e.Login);

        //SecurityLoginsRolePoco <<- (Login) -> SecurityRolePoco
        modelBuilder.Entity<SecurityLoginsRolePoco>()
          .HasOne(e => e.SecurityRole)
          .WithMany(e => e.SecurityLoginsRoles)
          .HasForeignKey(e => e.Login);
        #endregion
    }
}