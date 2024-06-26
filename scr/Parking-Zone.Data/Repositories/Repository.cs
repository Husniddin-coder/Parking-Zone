﻿using Microsoft.EntityFrameworkCore;
using Parking_Zone.Data.DbContexts;
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

    public void Create(T entity)
    {
        var entry = _dbSet.Add(entity);

        _dbcontext.SaveChanges();
    }

    public void Delete(long id)
    {
        var entry = _dbSet.FirstOrDefault(x => x.Id == id);

        _dbSet.Remove(entry);

       _dbcontext.SaveChanges();
    }

    public IEnumerable<T> GetAll()
        => _dbSet;

    public T Get(long? id)
        => _dbSet.FirstOrDefault(e => e.Id == id);

    public void Update(T entity)
    {
        var entry = _dbcontext.Update(entity);

        _dbcontext.SaveChanges();
    }
}
