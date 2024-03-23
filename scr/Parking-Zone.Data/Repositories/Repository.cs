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

    public async Task<T> CreateAsync(T entity)
    {
        var entry = await _dbSet.AddAsync(entity);

        await _dbcontext.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var entry = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        _dbSet.Remove(entry);

        return await _dbcontext.SaveChangesAsync() > 0;
    }

    public IQueryable<T> GetAllAsync()
        => _dbSet;

    public async Task<T> GetAsync(long id)
        => await _dbSet.FirstOrDefaultAsync(e => e.Id == id);

    public async Task<T> UpdateAsync(T entity)
    {
        var entry =  _dbcontext.Update(entity);

        await _dbcontext.SaveChangesAsync();

        return entry.Entity;
    }
}
