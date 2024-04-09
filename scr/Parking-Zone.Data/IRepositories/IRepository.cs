using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Data.IRepositories;

public interface IRepository<T> where T : Auditable
{
    void Create(T entity);

    void Update(T entity);

    void Delete(long id);

    T Get(long? id);

    IEnumerable<T> GetAll();
}
