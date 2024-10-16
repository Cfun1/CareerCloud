using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CareerCloud.EntityFrameworkDataAccess;
public class LoggingSaveChangesInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        var dbContext = eventData.Context;
        if (dbContext is null)
            return base.SavingChanges(eventData, result);

        var addedCount = dbContext.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added).Count();

        var modifiedCount = dbContext.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified).Count();

        var deletedCount = dbContext.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Deleted).Count();


        Debug.WriteLine("SavingChanges() called...");
        Debug.WriteLine("_______________________________");
        Debug.WriteLine($"Entities Added: {addedCount}");
        Debug.WriteLine($"Entities Modified: {modifiedCount}");
        Debug.WriteLine($"Entities deleted: {deletedCount}");
        Debug.WriteLine("_______________________________\r\n");

        return base.SavingChanges(eventData, result);
    }
}