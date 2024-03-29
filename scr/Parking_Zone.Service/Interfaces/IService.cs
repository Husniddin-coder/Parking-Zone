using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Service.Interfaces;

public interface IService<T> where T : Auditable
{
    T RetrieveById(long? id);

    IEnumerable<T> RetrieveAll();

    T Insert(T entity);

    T Modify(T entity);

    bool Remove(long id);
}
