﻿using System.Linq.Expressions;
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
    public CareerCloudContext context;
    public DbSet<T> dbSetT;

    public EFGenericRepository()
    {
        context = new CareerCloudContext();
        dbSetT = context.Set<T>();
    }

    int CallSaveChanges()
    {
        try
        {
            var recordCount = context.SaveChanges();
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

        //todo: just for test limit top 50 no need tireturn 99999 records
        var entitiesList = dbSetT
                            .Take(50)       //used to implement Keyset pagination .where(id >..)
                            .ToList();

        if (entitiesList is null)
            throw new InvalidOperationException("No entities found that matches the given predicate.");

        return entitiesList;
    }

    public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
    {
        if (where is null)
            throw new ArgumentNullException(nameof(where));

        var linqQueryT = dbSetT.Where(where);
        foreach (var navigationProp in navigationProperties)
            if (navigationProp is not null)
                linqQueryT.Include(navigationProp);

        var entitiesList = linqQueryT.ToList();
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

    public void Dispose() => context?.Dispose();
}