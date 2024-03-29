using Microsoft.EntityFrameworkCore;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Auditable
{
    protected readonly ApplicationDbContext _dbcontext;

    protected readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext dbcontext)
    {
        _dbcontext = dbcontext;
        _dbSet = _dbcontext.Set<T>();
    }

    public T Create(T entity)
    {
        var entry =  _dbSet.Add(entity);

         _dbcontext.SaveChanges();

        return entry.Entity;
    }

    public bool Delete(long id)
    {
        var entry =  _dbSet.FirstOrDefault(x => x.Id == id);

        _dbSet.Remove(entry);

        return  _dbcontext.SaveChanges() > 0;
    }

    public IQueryable<T> GetAll()
        => _dbSet;

    public T Get(long? id)
        =>  _dbSet.FirstOrDefault(e => e.Id == id);

    public T Update(T entity)
    {
        var entry =  _dbcontext.Update(entity);

         _dbcontext.SaveChanges();

        return entry.Entity;
    }
}
