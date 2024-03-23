using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Data.IRepositories;

public interface IRepository<T> where T : Auditable
{
    Task<T> CreateAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task<bool> DeleteAsync(long id);

    Task<T> GetAsync(long id);

    IQueryable<T> GetAllAsync();
}
