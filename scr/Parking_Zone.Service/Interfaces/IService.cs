using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Service.Interfaces;

public interface IService<T> where T : Auditable
{
    T RetrieveById(long? id);

    IEnumerable<T> RetrieveAll();

    void Insert(T entity);

    void Modify(T entity);

    void Remove(long id);
}
