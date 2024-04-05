using Shop.Domain.Abstraction;
using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces;

public interface IAddressRepository<TEntity>:IGenericRepository<TEntity> where TEntity:class,BaseEntity
{
    Task<List<TEntity>> GetAddressByUserId(Guid id);
}