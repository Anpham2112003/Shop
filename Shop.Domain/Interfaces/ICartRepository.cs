using Shop.Domain.Abstraction;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ICartRepository<TEntity>:IGenericRepository<TEntity> where TEntity : class,BaseEntity
    {
        Task<List<Cart>> GetCartByUserId(Guid id, int page, int skip);
        Task<int> CountCartByUserId(Guid id);
        Task<int> CountAsync();
    }
}
