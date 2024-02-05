using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IBrandRepository<Entity>:IGenericRepository<Entity> where Entity : class,BaseEntity
    {
        public Task<int> CountAsync();
        public Task<IEnumerable<Entity>> GetProductByBrandId(Guid Id,int page,int take);
    }
}
