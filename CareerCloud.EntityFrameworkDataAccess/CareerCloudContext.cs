using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess;

public class CareerCloudContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(DataAccessLayer.CommonDbConnection.String);
        base.OnConfiguring(optionsBuilder);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
