using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess;

/*  Future considerations:
*   Async methods, cancellation tokens
*   Centralized logging of certain exceptions or stats
*/
public class EFGenericRepository<T> : IDisposable, IDataRepository<T> where T : class
{
    //todo: temporary set to public for testing purposes, should be set back to private
    CareerCloudContext _context;
    DbSet<T> dbSetT;

    //only kept for Test project to run, remvoe for cloud to work
    //public EFGenericRepository()
    //{
    //    context = new CareerCloudContext();
    //    dbSetT = context.Set<T>();
    //}

    //depdency injection from API layer (conn string config/var)
    public EFGenericRepository(CareerCloudContext context)
    {
        _context = context;
        dbSetT = context.Set<T>();
    }

    int CallSaveChanges()
    {
        try
        {
            var recordCount = _context.SaveChanges();
            if (recordCount == 0)
                throw new InvalidOperationException("SaveChanges() operation called but 0 rows were affected");
            return recordCount;
        }

        //handle errors here
        catch (ObjectDisposedException) //inherit from InvalidOperationException
        {
            throw;
        }
        catch (InvalidOperationException)
        {
            throw;
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        catch (DbUpdateException)
        {
            throw;
        }

        //Base EF exception class
        //catch (DbUpdateException)
        //{
        //    throw;
        //}
    }

    public void Add(params T[] items)
    {
        if (!items.All(x => x != null))
            throw new ArgumentNullException(nameof(items));

        dbSetT.AddRange(items);
        CallSaveChanges();
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
    {
        foreach (var navigationProp in navigationProperties)
            if (navigationProp is not null)
                dbSetT.Include(navigationProp);

        //todo: just for test limit top 50 no need to return all records
        var entitiesList = dbSetT
                            .Take(50)  //used to implement Keyset pagination .where(id >..)
                            .ToList();

        if (entitiesList is null)
            throw new InvalidOperationException("No entities found that matches the given predicate.");

        return entitiesList;
    }

    //potentially could crash if DB compatibility level < SQL 2016 (no support for the new OPENJSON  )
    //https://learn.microsoft.com/en-us/ef/core/what-is-new/ef-core-8.0/breaking-changes#contains-in-linq-queries-may-stop-working-on-older-sql-server-versions
    public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
    {
        if (where is null)
            throw new ArgumentNullException(nameof(where));

        var linqQueryT = dbSetT.Where(where);
        foreach (var navigationProp in navigationProperties)
            if (navigationProp is not null)
                linqQueryT.Include(navigationProp);

        var entitiesList = linqQueryT?.ToList();
        if (entitiesList is null)
            throw new InvalidOperationException("No entities found that matches the given predicate.");

        return entitiesList;
    }

    public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
    {
        if (where is null)
            throw new ArgumentNullException(nameof(where));

        var linqQueryT = dbSetT.Where(where);
        foreach (var navigationProp in navigationProperties)
            if (navigationProp is not null)
                linqQueryT.Include(navigationProp);

        var entity = linqQueryT.FirstOrDefault();
        //test is expecting null value to be returned if no record found throwing exception
        //the method signature should be changed to [T? GetSingle] in the IDataRepository

        return entity;
    }

    public void Remove(params T[] items)
    {
        if (!items.All(x => x != null))
            throw new ArgumentNullException(nameof(items));

        var dbset = dbSetT.Where(c => items.Contains(c));
        dbSetT.RemoveRange(items);
        CallSaveChanges();
    }

    public void Update(params T[] items)
    {
        if (!items.All(x => x != null))
            throw new ArgumentNullException(nameof(items));

        dbSetT.UpdateRange(items);
        CallSaveChanges();
    }

    public void Dispose() => _context?.Dispose();
}
