using Shop.Domain.Abstraction;

namespace Shop.Domain.Interfaces;

public interface IRoleRepository<Entity>:IGenericRepository<Entity> where Entity:class,BaseEntity
{
    
}