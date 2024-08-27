using CareerCloud.Pocos;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess;
//TODO: future consideration
//AsSingleQuery()
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
                    .AddInterceptors(new LoggingSaveChangesInterceptor())
        //            .UseLazyLoadingProxies()
        //.LogTo(msg => Debug.WriteLine(msg), LogLevel.Information);
        ;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //no need for IsRequired(true/false) EF will make assumption based on nullability of type

        #region ApplicantProfilePoco
        modelBuilder.Entity<ApplicantProfilePoco>(builder =>
        {
            //ApplicantProfilePoco <- (Applicant) ->> ApplicantEducations
            builder
                .HasMany(e => e.ApplicantEducations)
                .WithOne(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant);

            //ApplicantProfilePoco  <- (Applicant) ->> ApplicantJobApplicationPoco
            builder
                .HasMany(e => e.ApplicantJobApplications)
                .WithOne(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant);

            //ApplicantProfilePoco <- (Applicant) ->> ApplicantResumePoco 
            builder
                .HasMany(e => e.ApplicantResumes)
                .WithOne(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant);

            //ApplicantProfilePoco  <- (Applicant) ->> ApplicantSkillPoco
            builder
                .HasMany(e => e.ApplicantSkills)
                .WithOne(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant);

            //ApplicantProfilePoco <- (Applicant) ->> ApplicantWorkHistoryPoco
            builder
                .HasMany(e => e.ApplicantWorkHistorys)
                .WithOne(e => e.ApplicantProfile)
                .HasForeignKey(e => e.Applicant);

            //note: should be one-to-one I believe: a single SecurityLogin reference a single applicant
            //ApplicantProfilePoco <<- (Login) -> SecurityLoginPoco
            builder
                .HasOne(e => e.SecurityLogin)
                .WithMany(e => e.ApplicantProfiles)
                .HasForeignKey(e => e.Login);

            //ApplicantProfilePoco <<- (Country) -> SystemCountryCodePoco
            builder
                .HasOne(e => e.SystemCountryCode)
                .WithMany(e => e.ApplicantProfiles)      //not required by test
                .HasForeignKey(e => e.Country);
        });
        #endregion


        #region CompanyDescriptionPoco
        //CompanyDescriptionPoco <<- (LanguageId) -> SystemLanguageCode
        modelBuilder.Entity<CompanyDescriptionPoco>()
          .HasOne(e => e.SystemLanguageCode)
          .WithMany(e => e.CompanyDescriptions)
          .HasForeignKey(e => e.LanguageId);
        #endregion


        #region CompanyJobPoco
        //CompanyJobPoco <- (Job) ->> ApplicantJobApplicationPoco
        modelBuilder.Entity<CompanyJobPoco>()
          .HasMany(e => e.ApplicantJobApplications)      //not required by test
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
        //CompanyProfilePoco <- (Company) ->> CompanyDescriptionPoco
        //note: many descriptions / company because in different languages
        modelBuilder.Entity<CompanyProfilePoco>()
          .HasMany(e => e.CompanyDescriptions)
          .WithOne(e => e.CompanyProfile)
          .HasForeignKey(e => e.Company);

        //CompanyProfilePoco  <- (Company) ->> CompanyJobPoco
        modelBuilder.Entity<CompanyProfilePoco>()
          .HasMany(e => e.CompanyJobs)
          .WithOne(e => e.CompanyProfile)
          .HasForeignKey(e => e.Company);

        //CompanyProfilePoco <- (Company) ->> CompanyLocationPoco
        modelBuilder.Entity<CompanyProfilePoco>()
          .HasMany(e => e.CompanyLocations)
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


        #region SystemCountryCodePoco
        //SystemCountryCodePoco <- (CountryCode) ->> ApplicantWorkHistoryPoco
        modelBuilder.Entity<SystemCountryCodePoco>()
          .HasMany(e => e.ApplicantWorkHistories)      //not required by test
          .WithOne(e => e.SystemCountryCode)
          .HasForeignKey(e => e.CountryCode);

        //SystemCountryCodePoco  <- (CountryCode) ->> CompanyLocationPoco
        modelBuilder.Entity<SystemCountryCodePoco>()
          .HasMany(e => e.CompanyLocations)      //not required by test
          .WithOne(e => e.SystemCountryCode)
          .HasForeignKey(e => e.CountryCode);
        #endregion
    }
}