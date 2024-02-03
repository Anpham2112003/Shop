using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IProductRepository<Entity>:IGenericRepository<Entity> where Entity : class,BaseEntity
    {
        Task<IEnumerable<Entity>> GetAllAsync(int page, int take);
        Task<int> Count();
    }
}
