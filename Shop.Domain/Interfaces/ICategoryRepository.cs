using Shop.Domain.Abstraction;
using Shop.Domain.Entities;
using Shop.Domain.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Interfaces
{
    public interface ICategoryRepository<Entity>:IGenericRepository<Entity> where Entity : class,BaseEntity
    {
        
        public Task<int> CountAsync();

        public Task AddCategoryToProduct(Guid CategoryId, Guid ProductId);

        public Task<IEnumerable<ProductPreviewResponseModel>?> GetProductInCategory(Guid CategoryId, int page, int take);

        public Task<int> CountProductInCategory(Guid CategoryId);

        public Task<ProductCategory?> FindProductCategory(Guid CategoryId, Guid ProductId);
        public void RemoveProductCatwgory(ProductCategory productCategory);
    }
}
