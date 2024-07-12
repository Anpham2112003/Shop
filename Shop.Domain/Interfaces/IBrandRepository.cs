using Shop.Domain.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;

namespace Shop.Domain.Interfaces
{
    public interface IBrandRepository<Entity>:IGenericRepository<Entity> where Entity : class,BaseEntity
    {
       
        public Task<List<Brand>> GetAllBrand( int page, int take);
        public Task<IEnumerable<ProductPreviewResponseModel>?> GetProductByBrandId(Guid id, int page, int take);
        public  Task<int> CountProductByBrandId(Guid id);
    }
}
