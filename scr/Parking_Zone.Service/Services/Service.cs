using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Service.Services;

public class Service<T> : IService<T> where T : Auditable
{
    private readonly IRepository<T> _repository;

    public Service(IRepository<T> repository)
     => _repository = repository;
    

    public virtual void Insert(T entity)
     => _repository.Create(entity);


    public void Modify(T entity)
     => _repository.Update(entity);


    public void Remove(long id)
     => _repository.Delete(id);

    public IEnumerable<T> RetrieveAll()
     => _repository.GetAll();

    public T RetrieveById(long? id)
     => _repository.Get(id);
}
