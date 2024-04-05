using Shop.Domain.Abstraction;

namespace Shop.Domain.Interfaces;

public interface IShipRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, BaseEntity
{
    
}