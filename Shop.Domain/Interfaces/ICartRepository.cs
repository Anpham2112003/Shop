using Shop.Domain.Abstraction;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ICartRepository<Entity>:IGenericRepository<Entity> where Entity : class,BaseEntity
    {
        Task<IEnumerable<Entity>> GetCartByUserId(Guid Id,int page,int skip);
        Task<int> CountAsync();
    }
}
