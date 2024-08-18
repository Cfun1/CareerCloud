using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.EntityFrameworkDataAccess;
public class EFGenericRepository<T> : IDisposable, IDataRepository<T> where T : class
{
    CareerCloudContext context;
    DbSet<T> dbSetT;

    public EFGenericRepository()
    {
        context = new CareerCloudContext();
        dbSetT = context.Set<T>();
    }

    public void Add(params T[] items)
    {
        dbSetT.AddRange(items);
        context.SaveChanges();
    }

    public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
    {
        throw new NotImplementedException();
    }

    public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
    {
        throw new NotImplementedException();
    }

    public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
    {
        return dbSetT.Where(where).ToList();
    }

    public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
    {
        return dbSetT.Where(where).FirstOrDefault();
    }

    public void Remove(params T[] items)
    {
        dbSetT.RemoveRange(items);
        context.SaveChanges();
    }

    public void Update(params T[] items)
    {
        dbSetT.UpdateRange(items);
        context.SaveChanges();
    }
    public void Dispose() => context?.Dispose();
}
