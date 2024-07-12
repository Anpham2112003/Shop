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
    public interface IProductRepository<Entity>:IGenericRepository<Entity> where Entity : class,BaseEntity
    {
        Task<ProductResponseModel?> GetProductDetails(Guid id);
        Task<List<ProductPreviewResponseModel>> GetProductPreview(int page, int take);
        public  Task<IEnumerable<ProductPreviewResponseModel>> SearchProduct(string Name, int skip, int take);


        Task<int> Count();
    }
}
