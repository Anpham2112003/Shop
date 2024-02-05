using Shop.Domain.Abstraction;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ICategoryRepository<Entity>:IGenericRepository<Entity> where Entity : class,BaseEntity
    {
        public Task<IEnumerable<Category>> GetProductsByCategoryId(Guid Id,int page,int take);
        public Task<int> CountAsync();
    }
}
