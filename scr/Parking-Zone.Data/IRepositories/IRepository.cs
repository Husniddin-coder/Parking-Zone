using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Data.IRepositories;

public interface IRepository<T> where T : Auditable
{
    T Create(T entity);

    T Update(T entity);

    bool Delete(long id);

    T Get(long id);

    IQueryable<T> GetAll();
}
