using Shop.Domain.Abstraction;

namespace Shop.Domain.Interfaces;

public interface IRole<Entity>:IGenericRepository<Entity> where Entity:class,BaseEntity
{
    
}