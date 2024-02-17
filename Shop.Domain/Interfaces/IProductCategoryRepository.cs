using Shop.Domain.Abstraction;
using Shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface IProductCategoryRepository<Entity>:IGenericRepository<Entity> where Entity : class,BaseEntity
    {
        public Task<int> CountAsync();
    }
}
