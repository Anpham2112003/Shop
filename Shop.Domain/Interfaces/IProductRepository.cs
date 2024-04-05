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
        Task<List<ProductResponseModel>?> GetAllAsync(int page, int take);
        Task<ProductResponseModel?> GetProductDetails(Guid id);
        Task<List<ProductPreviewResponseModel>> GetProductPreview(int page, int take);
        Task<List<GetProductByBrandIdResponseModel>> GetProductByBrandId(Guid id, int page, int take);

        Task<List<ProductPreviewResponseModel>> GetProductByCategoryId(Guid id ,int page ,int take);

        Task<int> CountProductByCategoryId(Guid id);

        Task<int> CountProductByBrandId(Guid id);
        Task<int> Count();
    }
}
