using System.Linq.Expressions;
using CareerCloud.DataAccessLayer;

namespace CareerCloud.EntityFrameworkDataAccess;
public class EFGenericRepository<T> : IDataRepository<T> where T : class
{
    public EFGenericRepository()
    {
        context = new CareerCloudContext();
    }

    CareerCloudContext context;

    public void Add(params T[] items)
    {
        context.AddRange(items);
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
        return context.Set<T>().Where(where).ToList();
    }

    public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
    {
        return context.Set<T>().Where(where).FirstOrDefault();
    }

    public void Remove(params T[] items)
    {
        context.Set<T>().RemoveRange(items);
        context.SaveChanges();
    }

    public void Update(params T[] items)
    {
        context.Set<T>().UpdateRange(items);
        context.SaveChanges();
    }
}
